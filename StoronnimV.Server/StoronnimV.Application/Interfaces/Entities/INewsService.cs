using StoronnimV.Application.Interfaces.Entities.Shared;
using StoronnimV.Domain.Projections.News;

namespace StoronnimV.Application.Interfaces.Entities;

public interface INewsService
    : IPaginationService<NewsPaginationProjection>, IAdminPaginationService<NewsFullProjection>, IGetByIdService<NewsFullProjection>
{
    
}