using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoronnimV.Application.Contracts.Controllers;
using StoronnimV.Application.DTO.Requests.Entities.Admin;
using StoronnimV.Application.DTO.Requests.Entities.Pages.Addition;
using StoronnimV.Application.DTO.Responses.Admin;

namespace StoronnimV.Api.Controllers
{
    /// <summary>
    /// Контроллер для админа, он позволяет управлять данными, которые отображаются на страницых (Удалять, изменять)
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/admin")]
    [ApiController]
    public class AdminController(
        IAdminControllerService adminControllerService) : ControllerBase
    {
        private readonly IAdminControllerService _adminControllerService = adminControllerService;

        [HttpDelete("news/{id:long}")]
        public async Task<IActionResult> DeleteNewsItem([FromRoute] long id, CancellationToken ct)
        {
            await _adminControllerService.DeleteNewsItemAsync(id, ct);

            return NoContent();
        }

        [HttpDelete("schedules/{id:long}")]
        public async Task<IActionResult> DeleteSchedule([FromRoute] long id, CancellationToken ct)
        {
            await _adminControllerService.DeleteScheduleAsync(id, ct);
            
            return NoContent();
        }

        [HttpDelete("videos/{id:long}")]
        public async Task<IActionResult> DeleteVideo([FromRoute] long id, CancellationToken ct)
        {
            await _adminControllerService.DeleteVideoAsync(id, ct);
            
            return NoContent();
        }

        [HttpDelete("group/{id:long}")]
        public async Task<IActionResult> DeleteGroup([FromRoute] long id, CancellationToken ct)
        {
            await _adminControllerService.DeleteGroupPageAsync(id, ct);
            
            return NoContent();
        }

        [HttpDelete("group/members/{id:long}")]
        public async Task<IActionResult> DeleteMember([FromRoute] long id, CancellationToken ct)
        {
            await _adminControllerService.DeleteMemberAsync(id, ct);
            
            return NoContent();
        }

        [HttpDelete("music/{id:long}")]
        public async Task<IActionResult> DeleteMusicPlatform([FromRoute] long id, CancellationToken ct)
        {
            await _adminControllerService.DeleteMusicPlatformAsync(id, ct);
            
            return NoContent();
        }

        [HttpDelete("socials/{id:long}")]
        public async Task<IActionResult> DeleteSocial([FromRoute] long id, CancellationToken ct)
        {
            await _adminControllerService.DeleteSocialAsync(id, ct);
            
            return NoContent();
        }
        
        [HttpPost("news")]
        public async Task<IActionResult> AddNewsItem([FromBody] NewsItemAdditionRequest request, CancellationToken ct)
        {
            await _adminControllerService.AddNewsItemAsync(request, ct);
            
            return NoContent();
        }
        
        [HttpPost("schedules")]
        public async Task<IActionResult> AddSchedule([FromBody] ScheduleAdditionRequest request, CancellationToken ct)
        {
            await _adminControllerService.AddScheduleAsync(request, ct);
            
            return NoContent();
        }

        [HttpPost("videos")]
        public async Task<IActionResult> AddVideo([FromBody] VideoAdditionRequest request, CancellationToken ct)
        {
            await _adminControllerService.AddVideoAsync(request, ct);
            
            return NoContent();
        }

        [HttpPost("group")]
        public async Task<IActionResult> AddGroup([FromBody] GroupPageAdditionRequest request, CancellationToken ct)
        {
            await _adminControllerService.AddGroupPageAsync(request, ct);
            
            return NoContent();
        }

        [HttpPost("group/members")]
        public async Task<IActionResult> AddMember([FromBody] MemberAdditionRequest request, CancellationToken ct)
        {
            await _adminControllerService.AddMemberAsync(request, ct);
            
            return NoContent();
        }

        [HttpPost("music")]
        public async Task<IActionResult> AddMusicPlatform([FromBody] MusicPlatformAdditionRequest request, CancellationToken ct)
        {
            await _adminControllerService.AddMusicPlatformAsync(request, ct);
            
            return NoContent();
        }

        [HttpPost("socials")]
        public async Task<IActionResult> AddSocial([FromBody] SocialAdditionRequest request, CancellationToken ct)
        {
            await _adminControllerService.AddSocialAsync(request, ct);
            
            return NoContent();
        }
    }
}
