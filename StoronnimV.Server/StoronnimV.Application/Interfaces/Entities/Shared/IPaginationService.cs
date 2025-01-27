using StoronnimV.Application.Models;

namespace StoronnimV.Application.Interfaces.Entities.Shared;

public interface IPaginationService
{
    Task<PaginationResult> GetForPageAsync(int page, int pageSize, CancellationToken ct, params object[] args);
}