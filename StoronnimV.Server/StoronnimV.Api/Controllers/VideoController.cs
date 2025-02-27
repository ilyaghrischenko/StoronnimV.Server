using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoronnimV.Application.Contracts.Controllers;
using StoronnimV.Application.DTO.Responses.SchedulePage;
using StoronnimV.Application.DTO.Responses.Shared;
using StoronnimV.Application.DTO.Responses.Video;
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
        IVideoControllerService videoControllerService)
        : ControllerBase
    {
        [HttpGet("{id:long}")]
        public async Task<ActionResult<VideoPageShortResponse>> GetVideo([FromRoute] long id, CancellationToken ct)
        {
            VideoPageShortResponse video = await videoControllerService.GetItemByIdAsync(id, ct);

            return Ok(video);
        }
        
        [HttpGet("page/{type}/{page:int}")]
        public async Task<ActionResult<PaginationResponse<VideoPageShortResponse>>> GetVideosForPage
            ([FromRoute] int page, [FromRoute] string type, CancellationToken ct, [FromQuery] int pageSize = 5)
        {
            var videosPaginationResponse = await videoControllerService.GetForPageAsync(page, pageSize, ct, type);
            
            return Ok(videosPaginationResponse);
        }
    }
}