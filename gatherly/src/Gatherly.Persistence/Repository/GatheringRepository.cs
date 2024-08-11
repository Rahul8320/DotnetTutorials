using Gatherly.Domain.Entities;
using Gatherly.Domain.Repositories;

namespace Gatherly.Persistence.Repository;

public class GatheringRepository : IGatheringRepository
{
    public void Add(Gathering gathering)
    {

    }

    public async Task<Gathering?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        await Task.Delay(1, cancellationToken);

        return null;
    }

    public async Task<Gathering?> GetByIdWithCreatorAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        await Task.Delay(1, cancellationToken);

        return null;
    }
}
