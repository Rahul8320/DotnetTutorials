using VerticalSlice.Api.Users.Infrastructure;

namespace VerticalSlice.Api.Users;

public sealed class RegisterUser(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher)
{
    public sealed record Request(string Email, string FirstName, string LastName, string Password);

    private static readonly SemaphoreSlim semaphore = new(1, 1);

    public async Task<User> Handle(Request request, CancellationToken cancellationToken)
    {
        if (await semaphore.WaitAsync(100, cancellationToken) == false)
        {
            throw new Exception("Please try again later.");
        }

        try
        {
            bool isEmailAlreadyExist = await userRepository.CheckEmailExistsAsync(request.Email, cancellationToken);

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
            await userRepository.CreateUserAndVerificationToken(user, cancellationToken);

            return user;
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            semaphore.Release();
        }
    }
}