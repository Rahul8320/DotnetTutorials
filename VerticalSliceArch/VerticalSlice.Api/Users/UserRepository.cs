using Microsoft.EntityFrameworkCore;
using VerticalSlice.Api.Data;

namespace VerticalSlice.Api.Users;

public sealed class UserRepository(AppDbContext context) : IUserRepository
{
    public async Task<bool> CheckEmailExistsAsync(string email)
    {
        return await context.Users.AnyAsync(u => u.Email == email);
    }

    public async Task InsertAsync(User user, CancellationToken cancellationToken)
    {
        context.Users.Add(user);
        await context.SaveChangesAsync(cancellationToken);
    }
}
