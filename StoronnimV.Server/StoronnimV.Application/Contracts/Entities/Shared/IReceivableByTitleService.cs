using StoronnimV.Domain.Projections.Shared;

namespace StoronnimV.Application.Contracts.Entities.Shared;

public interface IReceivableByTitleService<TProjection> where TProjection : BaseProjection
{
    Task<IEnumerable<TProjection>> GetItemsByTitleAsync(string title, CancellationToken ct);
}