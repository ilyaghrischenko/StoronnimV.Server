using StoronnimV.Domain.Enums;

namespace StoronnimV.Domain.Interfaces.Shared;

public interface IPaginationRepository
{
    Task<IEnumerable<object>?> GetForPageAsync(int page, CancellationToken ct, int pageSize = 10, params object[] args);
    Task<int> GetTotalCountAsync(CancellationToken ct, params object[] args);
}