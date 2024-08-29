using Microsoft.EntityFrameworkCore;
using VerticalSlice.Api.Users;

namespace VerticalSlice.IntegrationTests.UsersTests;

public class RegisterUserTests : IClassFixture<TestFixture>
{
    private readonly RegisterUser registerUser;
    private readonly TestFixture fixture;

    public RegisterUserTests(TestFixture fixture)
    {
        this.fixture = fixture;
        registerUser = new(this.fixture.UserRepository, new PasswordHasher());
    }

    [Fact]
    public async Task Handle_ShouldNotThrowException_WhenEmailIsUnique()
    {
        // Arrange
        string firstName = "John";
        string email = "test@example.com";
        string lastName = "Doe";
        string password = "password123";
        CancellationToken cancellationToken = new();

        await fixture.RemoveAllUsers();

        var request = new RegisterUser.Request(email, firstName, lastName, password);

        // Act
        var user = await registerUser.Handle(request, cancellationToken);

        // Assert
        Assert.NotNull(user);
        Assert.Equal(email, user.Email);
        Assert.Equal(firstName, user.FirstName);
        Assert.Equal(lastName, user.LastName);
        Assert.NotEqual(password, user.PasswordHash);
        var users = await fixture.Context.Users.ToListAsync();
        Assert.Single(users);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenEmailAlreadyExists()
    {
        // Arrange
        string firstName = "John";
        string email = "test@example.com";
        string lastName = "Doe";
        string password = "password123";
        CancellationToken cancellationToken = new();

        await fixture.RemoveAllUsers();

        var request = new RegisterUser.Request(firstName, lastName, email, password);

        // Simulate email already exists
        await registerUser.Handle(request, cancellationToken);

        // Act
        var exception = await Assert.ThrowsAsync<Exception>(
            () => registerUser.Handle(request, cancellationToken));

        // Assert
        Assert.Equal("This Email is already in use", exception.Message);

        var users = await fixture.Context.Users.ToListAsync();
        Assert.Single(users);
    }
}
