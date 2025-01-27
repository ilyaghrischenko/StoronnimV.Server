using AutoMapper;
using Microsoft.Extensions.Logging;
using StoronnimV.Application.DTO.Responses.HomePage;
using StoronnimV.Application.DTO.Responses.Video;
using StoronnimV.Application.Interfaces.Controllers;
using StoronnimV.Application.Interfaces.Home;

namespace StoronnimV.Application.Services.Controllers;

public class HomeControllerService(
    IHomeService homeService,
    ILogger<HomeControllerService> logger,
    IMapper mapper) : IHomeControllerService
{
    private readonly IHomeService _homeService = homeService;
    private readonly ILogger<HomeControllerService> _logger = logger;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<NewsHomeResponse>> GetNewsAsync(int count)
    {
        _logger.LogInformation($"Service: HomeControllerService Method: GetNewsAsync with count: {count} started at {DateTime.UtcNow}");

        var news = await _homeService.GetNewsForHomePageAsync(count);

        var newsDto = _mapper.Map<IEnumerable<NewsHomeResponse>>(news);
        
        _logger.LogInformation($"Service: HomeControllerService Method: GetNewsAsync with count: {count} ended at {DateTime.UtcNow}");
        
        return newsDto;
    }

    public async Task<ScheduleHomeResponse> GetScheduleAsync()
    {
        _logger.LogInformation($"Service: HomeControllerService Method: GetScheduleAsync started at {DateTime.UtcNow}");

        var schedule = await _homeService.GetScheduleForHomePageAsync();
        
        var scheduleDto = _mapper.Map<ScheduleHomeResponse>(schedule);
        
        _logger.LogInformation($"Service: HomeControllerService Method: GetScheduleAsync ended at {DateTime.UtcNow}");

        return scheduleDto;
    }

    public async Task<VideoPageShortResponse> GetVideoAsync()
    {
        _logger.LogInformation($"Service: HomeControllerService Method: GetVideoAsync started at {DateTime.UtcNow}");

        var promotionVideo = await _homeService.GetPromotionVideoForHomePageAsync();
        
        var promotionVideoDto = _mapper.Map<VideoPageShortResponse>(promotionVideo);
        
        _logger.LogInformation($"Service: HomeControllerService Method: GetVideoAsync ended at {DateTime.UtcNow}");
        
        return promotionVideoDto;
    }
}