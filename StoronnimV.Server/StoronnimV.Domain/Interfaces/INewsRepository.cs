using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Interfaces.Shared;
using StoronnimV.Domain.Projections.News;

namespace StoronnimV.Domain.Interfaces;

public interface INewsRepository
    : IRepository<News>, IGetByIdRepository<NewsFullProjection>,
        IPaginationRepository<NewsPaginationProjection>, IAdminPaginationRepository<NewsFullProjection>
{
    Task<IEnumerable<NewsHomeProjection>?> GetMainNewsForHomePageAsync(int count, CancellationToken ct);
}