using FluentAssertions;
using NSubstitute;
using VerticalSlice.Api.Users;

namespace VerticalSlice.UnitTest;

public class RegisterUserTests
{
    private readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();
    private readonly IPasswordHasher _passwordHasher = Substitute.For<IPasswordHasher>();
    private readonly RegisterUser _registerUser;

    public RegisterUserTests()
    {
        _registerUser = new RegisterUser(_userRepository, _passwordHasher);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenEmailAlreadyExists()
    {
        // Arrange
        var request = new RegisterUser.Request("test@example.com", "John", "Doe", "password123");
        _userRepository.CheckEmailExistsAsync(request.Email).Returns(true);

        // Act
        Func<Task> response = async () => await _registerUser.Handle(request, CancellationToken.None);

        // Assert
        await response.Should().ThrowAsync<Exception>()
            .WithMessage("This Email is already in use");

        await _userRepository.DidNotReceive().InsertAsync(Arg.Any<User>(), CancellationToken.None);
    }

    [Fact]
    public async Task Handle_ShouldCreateUser_WhenEmailDoesNotExist()
    {
        // Arrange
        var request = new RegisterUser.Request("newuser@example.com", "Jane", "Smith", "password123");
        _userRepository.CheckEmailExistsAsync(request.Email).Returns(false);
        _passwordHasher.Hash(request.Password).Returns("hashed_password");

        // Act
        var user = await _registerUser.Handle(request, CancellationToken.None);

        // Assert
        user.Should().NotBeNull();
        user.FirstName.Should().Be(request.FirstName);
        user.LastName.Should().Be(request.LastName);
        user.Email.Should().Be(request.Email);
        user.PasswordHash.Should().Be("hashed_password");

        await _userRepository.Received(1).InsertAsync(user, CancellationToken.None);
    }

    [Fact]
    public async Task Handle_ShouldCallInsertAsync_WhenUserIsCreated()
    {
        // Arrange
        var request = new RegisterUser.Request("newuser@example.com", "Jane", "Smith", "password123");
        _userRepository.CheckEmailExistsAsync(request.Email).Returns(false);
        _passwordHasher.Hash(request.Password).Returns("hashed_password");

        // Act
        await _registerUser.Handle(request, CancellationToken.None);

        // Assert
        await _userRepository.Received(1).InsertAsync(Arg.Is<User>(u =>
            u.FirstName == request.FirstName &&
            u.LastName == request.LastName &&
            u.Email == request.Email &&
            u.PasswordHash == "hashed_password"
        ), CancellationToken.None);
    }
}