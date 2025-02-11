using StoronnimV.Application.DTO.Responses.GroupPage;
using StoronnimV.Application.DTO.Responses.NewsPage;
using StoronnimV.Application.DTO.Responses.Shared;
using StoronnimV.Application.DTO.Responses.Video;

namespace StoronnimV.Application.Contracts.Controllers;

public interface IAdminControllerService
{
    Task<PaginationResponse<NewsResponse>> GetNewsForPageAsync(int page, int pageSize, CancellationToken ct, params object[] args);
    Task<PaginationResponse<VideoPageResponse>> GetVideosForPageAsync(int page, int pageSize, CancellationToken ct, params object[] args);
    Task<IEnumerable<NewsResponse>> GetNewsByTitleAsync(string title, CancellationToken ct);
    Task<IEnumerable<VideoPageResponse>> GetVideosByTitleAsync(string title, CancellationToken ct);
    Task<AdminGroupPageFullResponse> GetGroupInfoAsync(CancellationToken ct);
}