using StoronnimV.Application.Models;

namespace StoronnimV.Application.Interfaces.Entities.Shared;

public interface IAdminPaginationService
{
    Task<PaginationResult> GetForAdminPageAsync(int page, int pageSize, CancellationToken ct, params object[] args);
}