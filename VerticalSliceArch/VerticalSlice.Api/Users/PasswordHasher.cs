namespace VerticalSlice.Api.Users;

public sealed class PasswordHasher : IPasswordHasher
{
    public string Hash(string password)
    {
        return password;
    }
}
