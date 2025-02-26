using StoronnimV.Application.Contracts.Entities.Shared;
using StoronnimV.Application.DTO.Requests.Entities.Pages.Addition;
using StoronnimV.Domain.Projections.News;

namespace StoronnimV.Application.Contracts.Entities;

public interface INewsService
    : IPaginationService<NewsPaginationProjection>, IGetByIdService<NewsFullProjection>
{
    Task AddNewsItemAsync(NewsItemAdditionRequest request, CancellationToken ct);
    Task DeleteNewsItemAsync(long id, CancellationToken ct);
    
    //TODO: Добавить обновление новости
    // Task UpdateNewsItemAsync(NewsItemUpdateRequest request, CancellationToken ct);
}