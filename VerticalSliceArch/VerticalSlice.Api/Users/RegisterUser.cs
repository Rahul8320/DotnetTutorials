namespace VerticalSlice.Api.Users;

public sealed class RegisterUser(IUserRepository userRepository, IPasswordHasher passwordHasher)
{
    public record Request(string Email, string FirstName, string LastName, string Password);

    public async Task<User> Handle(Request request, CancellationToken cancellationToken)
    {
        bool isEmailAlreadyExist = await userRepository.CheckEmailExistsAsync(request.Email);

        if (isEmailAlreadyExist)
        {
            throw new Exception("This Email is already in use");
        }

        // creating a user
        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PasswordHash = passwordHasher.Hash(request.Password),
        };

        // store in db
        await userRepository.InsertAsync(user, cancellationToken);

        return user;
    }
}