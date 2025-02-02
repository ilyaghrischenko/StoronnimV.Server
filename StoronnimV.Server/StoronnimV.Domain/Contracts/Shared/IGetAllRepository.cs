using StoronnimV.Domain.Projections.Shared;

namespace StoronnimV.Domain.Contracts.Shared;

public interface IGetAllRepository<TProjection> where TProjection : BaseProjection
{
    Task<IEnumerable<TProjection>?> GetAllAsNoTrackingAsync(CancellationToken ct);
}