using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoronnimV.Application.Contracts.Controllers;
using StoronnimV.Application.DTO.Responses.MusicPage;

namespace StoronnimV.Api.Controllers
{
    [Route("api/music")]
    [ApiController]
    public class MusicController(IMusicControllerService musicControllerService) : ControllerBase
    {
        private readonly IMusicControllerService _musicControllerService = musicControllerService;

        [HttpGet("{id:long}")]
        public async Task<ActionResult<MusicResponse>> GetMusicPlatform([FromRoute] long id, CancellationToken ct)
        {
            MusicResponse musicPlatform = await _musicControllerService.GetItemByIdAsync(id, ct);

            return Ok(musicPlatform);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MusicResponse>>> GetMusicPlatforms(CancellationToken ct)
        {
            var musicPlatforms = await _musicControllerService.GetAllAsync(ct);
            
            return Ok(musicPlatforms);
        }
    }
}
