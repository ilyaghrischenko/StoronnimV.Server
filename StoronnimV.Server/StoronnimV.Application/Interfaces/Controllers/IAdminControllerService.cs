using StoronnimV.Application.DTO.Responses.NewsPage;
using StoronnimV.Application.DTO.Responses.Shared;
using StoronnimV.Application.DTO.Responses.Video;
using StoronnimV.Application.Interfaces.Controllers.Shared;

namespace StoronnimV.Application.Interfaces.Controllers;

public interface IAdminControllerService
{
    Task<PaginationResponse<NewsResponse>> GetNewsForPageAsync(int page, int pageSize, params object[] args);
    Task<PaginationResponse<VideoPageResponse>> GetVideosForPageAsync(int page, int pageSize, params object[] args);
}