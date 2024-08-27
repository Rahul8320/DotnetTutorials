namespace VerticalSlice.Api.Users;

public interface IUserRepository
{
    Task<bool> CheckEmailExistsAsync(string email);
    Task InsertAsync(User user, CancellationToken cancellationToken);
}
