namespace VerticalSlice.Api.Users;

public sealed class LoginUser(IUserRepository userRepository, IPasswordHasher passwordHasher)
{
    public sealed record Request(string Email, string Password);

    public async Task<User> Handle(Request request)
    {
        // Get user by email
        User? user = await userRepository.GetByEmail(request.Email) ?? throw new Exception("User not found");

        // verify password
        bool isVerified = passwordHasher.Verify(request.Password, user.PasswordHash);

        if (isVerified == false)
        {
            throw new Exception("Password is wrong");
        }

        return user;
    }
}
