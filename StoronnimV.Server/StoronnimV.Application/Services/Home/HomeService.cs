using Microsoft.Extensions.Logging;
using StoronnimV.Application.Contracts.Home;
using StoronnimV.Domain.Contracts;
using StoronnimV.Domain.Contracts.Database;
using StoronnimV.Domain.Projections.News;
using StoronnimV.Domain.Projections.Schedule;
using StoronnimV.Domain.Projections.Video;

namespace StoronnimV.Application.Services.Home;

public class HomeService(
    INewsRepository newsRepository,
    IScheduleRepository scheduleRepository,
    IVideoRepository videoRepository) : IHomeService
{
    private readonly INewsRepository _newsRepository = newsRepository;
    private readonly IScheduleRepository _scheduleRepository = scheduleRepository;
    private readonly IVideoRepository _videoRepository = videoRepository;
    
    public async Task<IEnumerable<NewsHomeProjection>> GetMainNewsForHomePageAsync(int count, CancellationToken ct)
    {
        var newsForHomePage = await _newsRepository.GetMainNewsForHomePageAsync(count, ct);
        if (newsForHomePage is null || !newsForHomePage.Any())
        {
            return new List<NewsHomeProjection>();
        }
        
        return newsForHomePage
            .ToList();
    }

    public async Task<ScheduleShortProjection?> GetNearestScheduleForHomePageAsync(CancellationToken ct)
    {
        ScheduleShortProjection? schedule = await _scheduleRepository.GetNearestScheduleForHomePageAsync(ct);
        
        return schedule;
    }

    public async Task<VideoShortProjection?> GetPromotionVideoForHomePageAsync(CancellationToken ct)
    {
        VideoShortProjection? promotionVideo = await _videoRepository.GetPromotionVideoForHomePageAsync(ct);
        
        return promotionVideo;
    }
}