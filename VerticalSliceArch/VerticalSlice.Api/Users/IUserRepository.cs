namespace VerticalSlice.Api.Users;

public interface IUserRepository
{
    Task<bool> CheckEmailExistsAsync(string email, CancellationToken cancellationToken = default);
    Task<User?> GetByEmail(string email);
    Task InsertAsync(User user, CancellationToken cancellationToken);
}
