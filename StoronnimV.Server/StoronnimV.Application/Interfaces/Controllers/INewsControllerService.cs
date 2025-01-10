using StoronnimV.Application.DTO.Responses.NewsPage;
using StoronnimV.Application.Interfaces.Controllers.Shared;

namespace StoronnimV.Application.Interfaces.Controllers;

public interface INewsControllerService : IPaginationControllerService<NewsShortResponse>
{
    Task<NewsResponse> GetItemByIdAsync(long id);
    Task<IEnumerable<NewsResponse>> GetAllAsync();
}