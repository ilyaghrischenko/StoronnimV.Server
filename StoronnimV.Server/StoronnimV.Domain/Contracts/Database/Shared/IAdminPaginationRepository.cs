using StoronnimV.Domain.Projections.Shared;

namespace StoronnimV.Domain.Contracts.Database.Shared;

public interface IAdminPaginationRepository<TProjection> where TProjection : BaseProjection
{
    Task<IEnumerable<TProjection>?> GetForAdminPageAsync(int page, CancellationToken ct, int pageSize = 10, params object[] args);
}