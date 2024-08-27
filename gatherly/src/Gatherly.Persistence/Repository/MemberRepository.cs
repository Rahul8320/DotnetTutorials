using Gatherly.Domain.Entities;
using Gatherly.Domain.Repositories;
using Gatherly.Domain.ValueObjects;
using Gatherly.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Gatherly.Persistence.Repository;

public class MemberRepository(AppDbContext context) : IMemberRepository
{
    public void Add(Member member)
    {
        context.Members.Add(member);
        return;
    }

    public async Task<Member?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var member = await context.Members.FindAsync([id], cancellationToken);

        return member;
    }

    public async Task<bool> IsEmailUnique(Email email, CancellationToken cancellationToken = default)
    {
        var memberWithEmail = await context.Members.FirstOrDefaultAsync(
            x => x.Email.Equals(email), cancellationToken);

        return memberWithEmail is null;
    }
}
