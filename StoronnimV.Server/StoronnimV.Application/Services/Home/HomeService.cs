using Microsoft.Extensions.Logging;
using StoronnimV.Application.Interfaces.Home;
using StoronnimV.Domain.Interfaces;
using StoronnimV.Domain.Projections.News;
using StoronnimV.Domain.Projections.Schedule;
using StoronnimV.Domain.Projections.Video;

namespace StoronnimV.Application.Services.Home;

public class HomeService(
    INewsRepository newsRepository,
    IScheduleRepository scheduleRepository,
    IVideoRepository videoRepository,
    ILogger<HomeService> logger) : IHomeService
{
    private readonly INewsRepository _newsRepository = newsRepository;
    private readonly IScheduleRepository _scheduleRepository = scheduleRepository;
    private readonly IVideoRepository _videoRepository = videoRepository;
    private readonly ILogger<HomeService> _logger = logger;
    
    public async Task<IEnumerable<NewsHomeProjection>> GetMainNewsForHomePageAsync(int count, CancellationToken ct)
    {
        _logger.LogInformation($"Service: HomeService Method: GetNewsForHomePageAsync with count: {count} started at {DateTime.UtcNow}");
        
        var newsForHomePage = await _newsRepository.GetMainNewsForHomePageAsync(count, ct);
        if (newsForHomePage is null || !newsForHomePage.Any())
        {
            return new List<NewsHomeProjection>();
        }
        
        _logger.LogInformation($"Service: HomeService Method: GetNewsForHomePageAsync with count: {count} ended at {DateTime.UtcNow}");
        
        return newsForHomePage
            .ToList();
    }

    public async Task<ScheduleShortProjection?> GetNearestScheduleForHomePageAsync(CancellationToken ct)
    {
        _logger.LogInformation($"Service: HomeService Method: GetScheduleForHomePageAsync started at {DateTime.UtcNow}");
        
        ScheduleShortProjection? schedule = await _scheduleRepository.GetNearestScheduleForHomePageAsync(ct);
        
        _logger.LogInformation($"Service: HomeService Method: GetScheduleForHomePageAsync ended at {DateTime.UtcNow}");

        return schedule;
    }

    public async Task<VideoShortProjection?> GetPromotionVideoForHomePageAsync(CancellationToken ct)
    {
        _logger.LogInformation($"Service: HomeService Method: GetScheduleForHomePageAsync started at {DateTime.UtcNow}");

        VideoShortProjection? promotionVideo = await _videoRepository.GetPromotionVideoForHomePageAsync(ct);
        
        _logger.LogInformation($"Service: HomeService Method: GetScheduleForHomePageAsync ended at {DateTime.UtcNow}");
        
        return promotionVideo;
    }
}