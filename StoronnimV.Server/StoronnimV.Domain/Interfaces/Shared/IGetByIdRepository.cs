using StoronnimV.Domain.Entities.Shared;
using StoronnimV.Domain.Projections.Shared;

namespace StoronnimV.Domain.Interfaces.Shared;

public interface IGetByIdRepository<TProjection> where TProjection : BaseProjection
{
    Task<TProjection?> GetByIdAsNoTrackingAsync(long id, CancellationToken ct);
}