using StoronnimV.Domain.Projections.Shared;

namespace StoronnimV.Application.Contracts.Entities.Shared;

public interface IAdminGetAllService<TProjection> where TProjection : BaseProjection
{
    Task<IEnumerable<TProjection>> GetAllForAdminAsync(CancellationToken ct);
}