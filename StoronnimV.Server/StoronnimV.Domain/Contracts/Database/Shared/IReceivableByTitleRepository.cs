using StoronnimV.Domain.Projections.Shared;

namespace StoronnimV.Domain.Contracts.Database.Shared;

public interface IReceivableByTitleRepository<TProjection> where TProjection : BaseProjection
{
    Task<IEnumerable<TProjection>?> GetItemsByTitle(string title, CancellationToken ct);
}