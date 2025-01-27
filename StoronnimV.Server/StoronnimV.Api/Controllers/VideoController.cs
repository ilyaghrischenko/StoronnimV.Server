using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoronnimV.Application.DTO.Responses.SchedulePage;
using StoronnimV.Application.DTO.Responses.Shared;
using StoronnimV.Application.DTO.Responses.Video;
using StoronnimV.Application.Interfaces.Controllers;
using StoronnimV.Domain.Entities;

namespace StoronnimV.Api.Controllers
{
    /// <summary>
    /// Контроллер для страницы 'Видео', он позволяет доставать нужные данные для отображения
    /// </summary>
    /// <param name="videoControllerService"></param>
    /// <param name="logger"></param>
    [Route("api/videos")]
    [ApiController]
    public class VideoController(
        IVideoControllerService videoControllerService,
        ILogger<VideoController> logger)
        : ControllerBase
    {
        private readonly IVideoControllerService _videoControllerService = videoControllerService;
        private readonly ILogger<VideoController> _logger = logger;

        [HttpGet("{id:long}")]
        public async Task<ActionResult<VideoPageShortResponse>> GetVideo([FromRoute] long id, CancellationToken ct)
        {
            _logger.LogInformation(
                $"Controller: VideoController Method: GetVideo with id: {id} started at {DateTime.UtcNow}");

            var video = await _videoControllerService.GetItemByIdAsync(id, ct);

            _logger.LogInformation(
                $"Controller: VideoController Method: GetVideo with id: {id} ended at {DateTime.UtcNow}");

            return Ok(video);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VideoPageShortResponse>>> GetVideos(CancellationToken ct)
        {
            _logger.LogInformation($"Controller: VideoController Method: GetVideos started at {DateTime.UtcNow}");

            var videos = await _videoControllerService.GetAllAsync(ct);

            _logger.LogInformation($"Controller: VideoController Method: GetVideos ended at {DateTime.UtcNow}");

            return Ok(videos);
        }
        
        [HttpGet("page/{type}/{page:int}")]
        public async Task<ActionResult<PaginationResponse<VideoPageShortResponse>>> GetVideosForPage
            ([FromRoute] int page, [FromRoute] string type, CancellationToken ct, [FromQuery] int pageSize = 5)
        {
            _logger.LogInformation($"Controller: NewsController Method: GetNewsForPage with page: {page} started at {DateTime.UtcNow}");
            
            var videosPaginationResponse = await _videoControllerService.GetForPageAsync(page, pageSize, ct, type);
            
            _logger.LogInformation($"Controller: NewsController Method: GetNewsForPage with page: {page} ended at {DateTime.UtcNow}");
            
            return Ok(videosPaginationResponse);
        }
    }
}