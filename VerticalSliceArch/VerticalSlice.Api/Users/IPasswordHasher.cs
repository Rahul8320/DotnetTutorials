
namespace VerticalSlice.Api.Users;

public interface IPasswordHasher
{
    string Hash(string password);
    bool Verify(string password, string passwordHash);
}