using Gatherly.Domain.Entities;
using Gatherly.Domain.Repositories;
using Gatherly.Domain.ValueObjects;

namespace Gatherly.Persistence.Repository;

public class MemberRepository : IMemberRepository
{
    public void Add(Member member)
    {

    }

    public async Task<Member?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await Task.Delay(1, cancellationToken);

        return null;
    }

    public async Task<bool> IsEmailUnique(Email email, CancellationToken cancellationToken = default)
    {
        await Task.Delay(1, cancellationToken);

        return true;
    }
}
