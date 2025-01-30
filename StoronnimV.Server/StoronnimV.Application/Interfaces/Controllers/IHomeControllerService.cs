using StoronnimV.Application.DTO.Responses.HomePage;
using StoronnimV.Application.DTO.Responses.Video;

namespace StoronnimV.Application.Interfaces.Controllers;

public interface IHomeControllerService
{
    Task<IEnumerable<NewsHomeResponse>> GetMainNewsAsync(int count, CancellationToken ct);
    Task<ScheduleHomeResponse> GetNearestScheduleAsync(CancellationToken ct);
    Task<VideoPageShortResponse> GetPromotionVideoAsync(CancellationToken ct);
}