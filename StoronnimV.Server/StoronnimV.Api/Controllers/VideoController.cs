using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoronnimV.Application.DTO.Responses.SchedulePage;
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
        public async Task<ActionResult<VideoPageResponse>> GetVideo([FromRoute] long id)
        {
            _logger.LogInformation(
                $"Controller: VideoController Method: GetVideo with id: {id} started at {DateTime.UtcNow}");

            var video = await _videoControllerService.GetItemByIdAsync(id);

            _logger.LogInformation(
                $"Controller: VideoController Method: GetVideo with id: {id} ended at {DateTime.UtcNow}");

            return Ok(video);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VideoPageResponse>>> GetVideos()
        {
            _logger.LogInformation($"Controller: VideoController Method: GetVideos started at {DateTime.UtcNow}");

            var videos = await _videoControllerService.GetAllAsync();

            _logger.LogInformation($"Controller: VideoController Method: GetVideos ended at {DateTime.UtcNow}");

            return Ok(videos);
        }
    }
}