using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using VerticalSlice.Api.Users;

namespace VerticalSlice.UnitTest.UsersTests;

public class LoginUserTests
{
    private readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();
    private readonly IPasswordHasher _passwordHasher = Substitute.For<IPasswordHasher>();
    private readonly LoginUser _loginUser;

    public LoginUserTests()
    {
        _loginUser = new LoginUser(_userRepository, _passwordHasher);
    }

    [Fact]
    public async Task Handle_ShouldReturnUser_WhenCredentialsAreValid()
    {
        // Arrange
        var request = new LoginUser.Request("user@example.com", "password123");
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            FirstName = "test",
            LastName = "test",
            PasswordHash = _passwordHasher.Hash(request.Password)
        };
        _userRepository.GetByEmail(request.Email).Returns(user);
        _passwordHasher.Verify(request.Password, user.PasswordHash).Returns(true);

        // Act
        var result = await _loginUser.Handle(request);

        // Assert
        result.Should().BeEquivalentTo(user);
        await _userRepository.Received(1).GetByEmail(request.Email);
        await _userRepository.DidNotReceive().InsertAsync(user, CancellationToken.None);
        _passwordHasher.Received(1).Verify(request.Password, user.PasswordHash);
        _passwordHasher.Received(1).Hash(request.Password);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenUserNotFound()
    {
        // Arrange
        var request = new LoginUser.Request("user@example.com", "password123");
        _userRepository.GetByEmail(request.Email).Returns((User?)null);

        // Act
        Func<Task> act = async () => await _loginUser.Handle(request);

        // Assert
        await act.Should().ThrowAsync<Exception>().WithMessage("User not found");
        await _userRepository.Received(1).GetByEmail(request.Email);
        _passwordHasher.DidNotReceive().Verify(request.Password, string.Empty);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenPasswordIsWrong()
    {
        // Arrange
        var request = new LoginUser.Request("user@example.com", "password123");
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            PasswordHash = _passwordHasher.Hash("correctPassword") // Hash for a different password
        };
        _userRepository.GetByEmail(request.Email).Returns(user);
        _passwordHasher.Verify(request.Password, user.PasswordHash).Returns(false);

        // Act
        Func<Task> act = async () => await _loginUser.Handle(request);

        // Assert
        await act.Should().ThrowAsync<Exception>().WithMessage("Password is wrong");
        await _userRepository.Received(1).GetByEmail(request.Email);
        _passwordHasher.Received(1).Verify(request.Password, user.PasswordHash);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenPasswordHasherThrowsException()
    {
        // Arrange
        var request = new LoginUser.Request("user@example.com", "password123");
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            PasswordHash = _passwordHasher.Hash(request.Password) // Correct hash
        };
        _userRepository.GetByEmail(request.Email).Returns(user);
        _passwordHasher.Verify(request.Password, user.PasswordHash).Throws(new InvalidOperationException("Hashing error"));

        // Act
        Func<Task> act = async () => await _loginUser.Handle(request);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("Hashing error");
    }
}
