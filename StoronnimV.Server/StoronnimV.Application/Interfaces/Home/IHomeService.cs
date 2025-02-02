using StoronnimV.Domain.Projections.News;
using StoronnimV.Domain.Projections.Schedule;
using StoronnimV.Domain.Projections.Video;

namespace StoronnimV.Application.Interfaces.Home;

public interface IHomeService
{
    Task<IEnumerable<NewsHomeProjection>> GetMainNewsForHomePageAsync(int count, CancellationToken ct);
    Task<ScheduleShortProjection?> GetNearestScheduleForHomePageAsync(CancellationToken ct);
    Task<VideoShortProjection?> GetPromotionVideoForHomePageAsync(CancellationToken ct);
}