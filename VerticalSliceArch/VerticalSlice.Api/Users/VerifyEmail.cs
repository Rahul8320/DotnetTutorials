using Microsoft.EntityFrameworkCore;
using VerticalSlice.Api.Data;

namespace VerticalSlice.Api.Users;

public sealed class VerifyEmail(AppDbContext context)
{
    public async Task<Results<bool>> Handle(Guid tokenId)
    {
        var token = await context.EmailVerificationTokens
            .Include(e => e.User)
            .FirstOrDefaultAsync(e => e.Id == tokenId);

        bool isValidToken = IsTokenValid(token);

        if (isValidToken == false)
        {
            return Results<bool>.BadRequest;
        }

        token!.User.EmailVerified = true;

        context.EmailVerificationTokens.Remove(token!);

        await context.SaveChangesAsync();

        return true;
    }

    private static bool IsTokenValid(EmailVerificationToken? token)
    {
        return token is not null &&
          token.ExpiresOnUtc >= DateTime.UtcNow &&
          token.User is not null &&
          token.User.EmailVerified == false;
    }
}
