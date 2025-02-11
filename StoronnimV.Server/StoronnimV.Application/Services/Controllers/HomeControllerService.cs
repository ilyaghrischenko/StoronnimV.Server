using AutoMapper;
using Microsoft.Extensions.Logging;
using StoronnimV.Application.Contracts.Controllers;
using StoronnimV.Application.Contracts.Home;
using StoronnimV.Application.DTO.Responses.HomePage;
using StoronnimV.Application.DTO.Responses.Video;
using StoronnimV.Domain.Projections.Schedule;
using StoronnimV.Domain.Projections.Video;

namespace StoronnimV.Application.Services.Controllers;

public class HomeControllerService(
    IHomeService homeService,
    ILogger<HomeControllerService> logger,
    IMapper mapper) : IHomeControllerService
{
    private readonly IHomeService _homeService = homeService;
    private readonly ILogger<HomeControllerService> _logger = logger;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<NewsHomeResponse>> GetMainNewsAsync(int count, CancellationToken ct)
    {
        _logger.LogInformation($"Service: HomeControllerService Method: GetNewsAsync with count: {count} started at {DateTime.UtcNow}");

        var news = await _homeService.GetMainNewsForHomePageAsync(count, ct);

        var newsDto = _mapper.Map<IEnumerable<NewsHomeResponse>>(news);
        
        _logger.LogInformation($"Service: HomeControllerService Method: GetNewsAsync with count: {count} ended at {DateTime.UtcNow}");
        
        return newsDto;
    }

    public async Task<ScheduleHomeResponse> GetNearestScheduleAsync(CancellationToken ct)
    {
        _logger.LogInformation($"Service: HomeControllerService Method: GetScheduleAsync started at {DateTime.UtcNow}");

        ScheduleShortProjection? schedule = await _homeService.GetNearestScheduleForHomePageAsync(ct);
        
        var scheduleDto = _mapper.Map<ScheduleHomeResponse>(schedule);
        
        _logger.LogInformation($"Service: HomeControllerService Method: GetScheduleAsync ended at {DateTime.UtcNow}");

        return scheduleDto;
    }

    public async Task<VideoPageShortResponse> GetPromotionVideoAsync(CancellationToken ct)
    {
        _logger.LogInformation($"Service: HomeControllerService Method: GetVideoAsync started at {DateTime.UtcNow}");

        VideoShortProjection? promotionVideo = await _homeService.GetPromotionVideoForHomePageAsync(ct);
        
        var promotionVideoDto = _mapper.Map<VideoPageShortResponse>(promotionVideo);
        
        _logger.LogInformation($"Service: HomeControllerService Method: GetVideoAsync ended at {DateTime.UtcNow}");
        
        return promotionVideoDto;
    }
}