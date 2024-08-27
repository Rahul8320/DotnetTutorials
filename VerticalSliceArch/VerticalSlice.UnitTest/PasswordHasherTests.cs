using FluentAssertions;
using VerticalSlice.Api.Users;

namespace VerticalSlice.UnitTest;

public class PasswordHasherTests
{
    private readonly PasswordHasher _passwordHasher;

    public PasswordHasherTests()
    {
        _passwordHasher = new PasswordHasher();
    }

    [Fact]
    public void Hash_ShouldReturnExpectedFormat()
    {
        // Arrange
        var password = "password123";

        // Act
        var result = _passwordHasher.Hash(password);

        // Assert
        result.Should().MatchRegex(@"^[0-9A-Fa-f]{64}-[0-9A-Fa-f]{32}$");
    }

    [Fact]
    public void Hash_ShouldReturnDifferentValuesForSamePassword()
    {
        // Arrange
        var password = "password123";

        // Act
        var hash1 = _passwordHasher.Hash(password);
        var hash2 = _passwordHasher.Hash(password);

        // Assert
        hash1.Should().NotBe(hash2);
    }

    [Fact]
    public void Hash_ShouldReturnDifferentValuesForDifferentPasswords()
    {
        // Arrange
        var password1 = "password123";
        var password2 = "differentpassword";

        // Act
        var hash1 = _passwordHasher.Hash(password1);
        var hash2 = _passwordHasher.Hash(password2);

        // Assert
        hash1.Should().NotBe(hash2);
    }

    [Fact]
    public void Verify_ShouldReturnTrue_ForCorrectPassword()
    {
        // Arrange
        var password = "password123";
        var passwordHash = _passwordHasher.Hash(password);

        // Act
        var result = _passwordHasher.Verify(password, passwordHash);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void Verify_ShouldReturnFalse_ForIncorrectPassword()
    {
        // Arrange
        var password = "password123";
        var incorrectPassword = "wrongpassword";
        var passwordHash = _passwordHasher.Hash(password);

        // Act
        var result = _passwordHasher.Verify(incorrectPassword, passwordHash);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void Verify_ShouldReturnFalse_ForCorruptedHash()
    {
        // Arrange
        var password = "password123";
        var passwordHash = _passwordHasher.Hash(password);

        // Corrupt the hash by modifying the string
        var corruptedHash = passwordHash.Replace('0', '1');

        // Act
        var result = _passwordHasher.Verify(password, corruptedHash);

        // Assert
        result.Should().BeFalse();
    }
}
