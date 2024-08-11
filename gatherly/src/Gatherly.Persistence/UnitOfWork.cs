using Gatherly.Domain.Repositories;
using Gatherly.Persistence.Data;

namespace Gatherly.Persistence;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
        return;
    }
}
