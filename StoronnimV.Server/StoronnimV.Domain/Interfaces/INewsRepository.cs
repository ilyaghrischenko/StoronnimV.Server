using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Interfaces.Shared;

namespace StoronnimV.Domain.Interfaces;

public interface INewsRepository
    : IRepository<News>, IReceivableRepository, IPaginationRepository, IAdminPaginationRepository
{
    Task<IEnumerable<object>?> GetMainNewsForHomePageAsync(int count, CancellationToken ct);
}