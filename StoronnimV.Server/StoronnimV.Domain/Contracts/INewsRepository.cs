using StoronnimV.Domain.Contracts.Shared;
using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Projections.News;

namespace StoronnimV.Domain.Contracts;

public interface INewsRepository
    : IRepository<News>, IGetByIdRepository<NewsFullProjection>,
        IPaginationRepository<NewsPaginationProjection>, IAdminPaginationRepository<NewsFullProjection>
{
    Task<IEnumerable<NewsHomeProjection>?> GetMainNewsForHomePageAsync(int count, CancellationToken ct);
}