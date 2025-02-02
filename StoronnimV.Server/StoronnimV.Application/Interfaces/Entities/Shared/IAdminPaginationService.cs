using StoronnimV.Application.Models;
using StoronnimV.Domain.Projections.Shared;

namespace StoronnimV.Application.Interfaces.Entities.Shared;

public interface IAdminPaginationService<TProjection> where TProjection : BaseProjection
{
    Task<PaginationResult<TProjection>> GetForAdminPageAsync(int page, int pageSize, CancellationToken ct, params object[] args);
}