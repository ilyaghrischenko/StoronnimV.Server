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
    IMapper mapper) : IHomeControllerService
{
    private readonly IHomeService _homeService = homeService;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<NewsHomeResponse>> GetMainNewsAsync(int count, CancellationToken ct)
    {
        var news = await _homeService.GetMainNewsForHomePageAsync(count, ct);

        var newsDto = _mapper.Map<IEnumerable<NewsHomeResponse>>(news);
        
        return newsDto;
    }

    public async Task<ScheduleHomeResponse> GetNearestScheduleAsync(CancellationToken ct)
    {
        ScheduleShortProjection? schedule = await _homeService.GetNearestScheduleForHomePageAsync(ct);
        
        var scheduleDto = _mapper.Map<ScheduleHomeResponse>(schedule);
        
        return scheduleDto;
    }

    public async Task<VideoPageShortResponse> GetPromotionVideoAsync(CancellationToken ct)
    {
        VideoShortProjection? promotionVideo = await _homeService.GetPromotionVideoForHomePageAsync(ct);
        
        var promotionVideoDto = _mapper.Map<VideoPageShortResponse>(promotionVideo);
        
        return promotionVideoDto;
    }
}