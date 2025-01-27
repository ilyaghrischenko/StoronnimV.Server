using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoronnimV.Application.DTO.Responses.MusicPage;
using StoronnimV.Application.Interfaces.Controllers;

namespace StoronnimV.Api.Controllers
{
    [Route("api/music")]
    [ApiController]
    public class MusicController(IMusicControllerService musicControllerService,
        ILogger<MusicController> logger) : ControllerBase
    {
        private readonly IMusicControllerService _musicControllerService = musicControllerService;
        private readonly ILogger<MusicController> _logger = logger;

        [HttpGet("{id:long}")]
        public async Task<ActionResult<MusicResponse>> GetMusicPlatform([FromRoute] long id, CancellationToken ct)
        {
            _logger.LogInformation($"Controller: MusicController Method: GetMusicPlatform with id: {id} started at {DateTime.UtcNow}");

            var musicPlatform = await _musicControllerService.GetItemByIdAsync(id, ct);
            
            _logger.LogInformation($"Controller: MusicController Method: GetMusicPlatform with id: {id} ended at {DateTime.UtcNow}");

            return Ok(musicPlatform);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MusicResponse>>> GetMusicPlatforms(CancellationToken ct)
        {
            _logger.LogInformation($"Controller: MusicController Method: GetMusicPlatforms started at {DateTime.UtcNow}");

            var musicPlatforms = await _musicControllerService.GetAllAsync(ct);
            
            _logger.LogInformation($"Controller: MusicController Method: GetMusicPlatforms ended at {DateTime.UtcNow}");
            
            return Ok(musicPlatforms);
        }
    }
}
