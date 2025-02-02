using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoronnimV.Application.Contracts.Controllers;
using StoronnimV.Application.DTO.Responses.HomePage;
using StoronnimV.Application.DTO.Responses.Video;

namespace StoronnimV.Api.Controllers
{
    [Route("api/home")]
    [ApiController]
    public class HomeController(
        IHomeControllerService homeControllerService,
        ILogger<HomeController> logger) : ControllerBase
    {
        private readonly IHomeControllerService _homeControllerService = homeControllerService;
        private readonly ILogger<HomeController> _logger = logger;

        [HttpGet("news/{count:int}")]
        public async Task<ActionResult<IEnumerable<NewsHomeResponse>>> GetMainNews([FromRoute] int count, CancellationToken ct)
        {
            _logger.LogInformation($"Controller: HomeController Method: GetNews with count: {count} started at {DateTime.UtcNow}");
            
            var newsDto = await _homeControllerService.GetMainNewsAsync(count, ct);
            
            _logger.LogInformation($"Controller: HomeController Method: GetNews with count: {count} ended at {DateTime.UtcNow}");

            return Ok(newsDto);
        }

        [HttpGet("schedule")]
        public async Task<ActionResult<ScheduleHomeResponse>> GetNearestSchedule(CancellationToken ct)
        {
            _logger.LogInformation($"Controller: HomeController Method: GetSchedule started at {DateTime.UtcNow}");
            
            ScheduleHomeResponse scheduleDto = await _homeControllerService.GetNearestScheduleAsync(ct);
            
            _logger.LogInformation($"Controller: HomeController Method: GetSchedule ended at {DateTime.UtcNow}");

            return Ok(scheduleDto);
        }

        [HttpGet("video")]
        public async Task<ActionResult<VideoPageShortResponse>> GetPromotionVideo(CancellationToken ct)
        {
            _logger.LogInformation($"Controller: HomeController Method: GetPromotionVideo started at {DateTime.UtcNow}");

            VideoPageShortResponse videoDto = await _homeControllerService.GetPromotionVideoAsync(ct);
            
            _logger.LogInformation($"Controller: HomeController Method: GetPromotionVideo ended at {DateTime.UtcNow}");

            return Ok(videoDto);
        }
    }
}
