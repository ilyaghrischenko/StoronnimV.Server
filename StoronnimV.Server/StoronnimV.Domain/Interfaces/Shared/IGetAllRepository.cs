using StoronnimV.Domain.Projections.Shared;

namespace StoronnimV.Domain.Interfaces.Shared;

public interface IGetAllRepository<TProjection> where TProjection : BaseProjection
{
    Task<IEnumerable<TProjection>?> GetAllAsNoTrackingAsync(CancellationToken ct);
}