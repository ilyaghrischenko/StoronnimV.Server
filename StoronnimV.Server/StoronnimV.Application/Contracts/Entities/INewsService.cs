using StoronnimV.Application.Contracts.Entities.Shared;
using StoronnimV.Domain.Projections.News;

namespace StoronnimV.Application.Contracts.Entities;

public interface INewsService
    : IPaginationService<NewsPaginationProjection>, IAdminPaginationService<NewsFullProjection>, IGetByIdService<NewsFullProjection>
{
    
}