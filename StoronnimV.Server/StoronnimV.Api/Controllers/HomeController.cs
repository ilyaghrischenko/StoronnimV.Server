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
        IHomeControllerService homeControllerService) : ControllerBase
    {
        private readonly IHomeControllerService _homeControllerService = homeControllerService;

        [HttpGet("news/{count:int}")]
        public async Task<ActionResult<IEnumerable<NewsHomeResponse>>> GetMainNews([FromRoute] int count, CancellationToken ct)
        {
            var newsDto = await _homeControllerService.GetMainNewsAsync(count, ct);

            return Ok(newsDto);
        }

        [HttpGet("schedule")]
        public async Task<ActionResult<ScheduleHomeResponse>> GetNearestSchedule(CancellationToken ct)
        {
            ScheduleHomeResponse scheduleDto = await _homeControllerService.GetNearestScheduleAsync(ct);

            return Ok(scheduleDto);
        }

        [HttpGet("video")]
        public async Task<ActionResult<VideoPageShortResponse>> GetPromotionVideo(CancellationToken ct)
        {
            VideoPageShortResponse videoDto = await _homeControllerService.GetPromotionVideoAsync(ct);

            return Ok(videoDto);
        }
    }
}
