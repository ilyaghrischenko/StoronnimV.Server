using StoronnimV.Domain.Projections.Shared;

namespace StoronnimV.Application.Interfaces.Entities.Shared;

public interface IGetAllService<TProjection> where TProjection : BaseProjection
{
    Task<IEnumerable<TProjection>> GetAllAsync(CancellationToken ct);
}