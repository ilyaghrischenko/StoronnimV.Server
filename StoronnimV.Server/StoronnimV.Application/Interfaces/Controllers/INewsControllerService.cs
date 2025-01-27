using StoronnimV.Application.DTO.Responses.NewsPage;
using StoronnimV.Application.DTO.Responses.Shared;
using StoronnimV.Application.Interfaces.Controllers.Shared;

namespace StoronnimV.Application.Interfaces.Controllers;

public interface INewsControllerService : IPaginationControllerService<PaginationResponse<NewsShortResponse>>
{
    Task<NewsResponse> GetItemByIdAsync(long id, CancellationToken ct);
    Task<IEnumerable<NewsResponse>> GetAllAsync(CancellationToken ct);
}