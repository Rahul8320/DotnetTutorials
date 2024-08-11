using Gatherly.Domain.Repositories;

namespace Gatherly.Persistence;

public class UnitOfWork : IUnitOfWork
{
    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}
