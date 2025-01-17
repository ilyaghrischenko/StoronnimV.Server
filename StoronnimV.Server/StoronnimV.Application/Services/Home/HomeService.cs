using Microsoft.Extensions.Logging;
using StoronnimV.Application.Extensions;
using StoronnimV.Application.Interfaces.Home;
using StoronnimV.Domain.Interfaces;

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
    
    public async Task<IEnumerable<object>> GetNewsForHomePageAsync(int count)
    {
        _logger.LogInformation($"Service: HomeService Method: GetNewsForHomePageAsync with count: {count} started at {DateTime.UtcNow}");
        
        var newsForHomePage = await _newsRepository.GetNewsForHomePageAsync(count);
        if (newsForHomePage is null || !newsForHomePage.Any())
        {
            return new List<object>();
        }
        
        _logger.LogInformation($"Service: HomeService Method: GetNewsForHomePageAsync with count: {count} ended at {DateTime.UtcNow}");
        
        return newsForHomePage
            .ToList();
    }

    public async Task<object?> GetScheduleForHomePageAsync()
    {
        _logger.LogInformation($"Service: HomeService Method: GetScheduleForHomePageAsync started at {DateTime.UtcNow}");
        
        var schedule = await _scheduleRepository.GetScheduleForHomePageAsync();
        
        _logger.LogInformation($"Service: HomeService Method: GetScheduleForHomePageAsync ended at {DateTime.UtcNow}");

        return schedule;
    }

    public async Task<object?> GetPromotionVideoForHomePageAsync()
    {
        _logger.LogInformation($"Service: HomeService Method: GetScheduleForHomePageAsync started at {DateTime.UtcNow}");

        var promotionVideo = await _videoRepository.GetPromotionVideoForHomePageAsync();
        
        _logger.LogInformation($"Service: HomeService Method: GetScheduleForHomePageAsync ended at {DateTime.UtcNow}");
        
        return promotionVideo;
    }
}