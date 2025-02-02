using StoronnimV.Domain.Projections.Shared;

namespace StoronnimV.Domain.Interfaces.Shared;

public interface IAdminPaginationRepository<TPojection> where TPojection : BaseProjection
{
    Task<IEnumerable<TPojection>?> GetForAdminPageAsync(int page, CancellationToken ct, int pageSize = 10, params object[] args);
}