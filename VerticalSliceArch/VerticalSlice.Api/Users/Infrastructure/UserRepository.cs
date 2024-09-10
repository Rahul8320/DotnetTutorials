using FluentEmail.Core;
using Microsoft.EntityFrameworkCore;
using VerticalSlice.Api.Data;

namespace VerticalSlice.Api.Users.Infrastructure;

public sealed class UserRepository(
    AppDbContext context,
    IFluentEmail fluentEmail,
    EmailVerificationLinkFactory emailVerificationLinkFactory) : IUserRepository
{
    public async Task<bool> CheckEmailExistsAsync(string email, CancellationToken cancellationToken = default)
    {
        return await context.Users.AnyAsync(u => u.Email == email, cancellationToken);
    }

    public async Task<User?> GetByEmail(string email)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task CreateUserAndVerificationToken(User user, CancellationToken cancellationToken)
    {
        context.Users.Add(user);

        DateTime utcNow = DateTime.UtcNow;
        var verificationToken = new EmailVerificationToken
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            CreatedOnUtc = utcNow,
            ExpiresOnUtc = utcNow.AddDays(1),
        };

        context.EmailVerificationTokens.Add(verificationToken);

        await context.SaveChangesAsync(cancellationToken);

        // !TODO: Please refactor this to register use case
        // Email verification email send
        var verificationLink = emailVerificationLinkFactory.Create(verificationToken);
        await fluentEmail
            .To(user.Email)
            .Subject("Email verification for Register Use Case")
            .Body($"To verify your email address, <a href='{verificationLink}'> click here. </a>", isHtml: true)
            .SendAsync();
    }
}

public interface IUserRepository
{
    Task<bool> CheckEmailExistsAsync(string email, CancellationToken cancellationToken = default);
    Task<User?> GetByEmail(string email);
    Task CreateUserAndVerificationToken(User user, CancellationToken cancellationToken);
}