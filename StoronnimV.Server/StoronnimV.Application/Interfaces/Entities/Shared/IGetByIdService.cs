using StoronnimV.Domain.Projections.Shared;

namespace StoronnimV.Application.Interfaces.Entities.Shared;

public interface IGetByIdService<TProjection> where TProjection : BaseProjection
{
    Task<TProjection> GetItemByIdAsync(long id, CancellationToken ct);
}