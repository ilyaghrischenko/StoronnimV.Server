using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
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
    [EnableRateLimiting("AdminLimit")]
    [Route("api/admin")]
    [ApiController]
    public class AdminController(
        IAdminControllerService adminControllerService) : ControllerBase
    {
        [HttpDelete("news/{id:long}")]
        public async Task<IActionResult> DeleteNewsItem([FromRoute] long id, CancellationToken ct)
        {
            await adminControllerService.DeleteNewsItemAsync(id, ct);

            return NoContent();
        }

        [HttpDelete("schedules/{id:long}")]
        public async Task<IActionResult> DeleteSchedule([FromRoute] long id, CancellationToken ct)
        {
            await adminControllerService.DeleteScheduleAsync(id, ct);
            
            return NoContent();
        }

        [HttpDelete("videos/{id:long}")]
        public async Task<IActionResult> DeleteVideo([FromRoute] long id, CancellationToken ct)
        {
            await adminControllerService.DeleteVideoAsync(id, ct);
            
            return NoContent();
        }

        [HttpDelete("group/{id:long}")]
        public async Task<IActionResult> DeleteGroup([FromRoute] long id, CancellationToken ct)
        {
            await adminControllerService.DeleteGroupPageAsync(id, ct);
            
            return NoContent();
        }

        [HttpDelete("group/members/{id:long}")]
        public async Task<IActionResult> DeleteMember([FromRoute] long id, CancellationToken ct)
        {
            await adminControllerService.DeleteMemberAsync(id, ct);
            
            return NoContent();
        }

        [HttpDelete("music/{id:long}")]
        public async Task<IActionResult> DeleteMusicPlatform([FromRoute] long id, CancellationToken ct)
        {
            await adminControllerService.DeleteMusicPlatformAsync(id, ct);
            
            return NoContent();
        }

        [HttpDelete("socials/{id:long}")]
        public async Task<IActionResult> DeleteSocial([FromRoute] long id, CancellationToken ct)
        {
            await adminControllerService.DeleteSocialAsync(id, ct);
            
            return NoContent();
        }
        
        [HttpPost("news")]
        public async Task<IActionResult> AddNewsItem([FromBody] NewsItemAdditionRequest request, CancellationToken ct)
        {
            await adminControllerService.AddNewsItemAsync(request, ct);
            
            return NoContent();
        }
        
        [HttpPost("schedules")]
        public async Task<IActionResult> AddSchedule([FromBody] ScheduleAdditionRequest request, CancellationToken ct)
        {
            await adminControllerService.AddScheduleAsync(request, ct);
            
            return NoContent();
        }

        [HttpPost("videos")]
        public async Task<IActionResult> AddVideo([FromBody] VideoAdditionRequest request, CancellationToken ct)
        {
            await adminControllerService.AddVideoAsync(request, ct);
            
            return NoContent();
        }

        [HttpPost("group")]
        public async Task<IActionResult> AddGroup([FromBody] GroupPageAdditionRequest request, CancellationToken ct)
        {
            await adminControllerService.AddGroupPageAsync(request, ct);
            
            return NoContent();
        }

        [HttpPost("group/members")]
        public async Task<IActionResult> AddMember([FromBody] MemberAdditionRequest request, CancellationToken ct)
        {
            await adminControllerService.AddMemberAsync(request, ct);
            
            return NoContent();
        }

        [HttpPost("music")]
        public async Task<IActionResult> AddMusicPlatform([FromBody] MusicPlatformAdditionRequest request, CancellationToken ct)
        {
            await adminControllerService.AddMusicPlatformAsync(request, ct);
            
            return NoContent();
        }

        [HttpPost("socials")]
        public async Task<IActionResult> AddSocial([FromBody] SocialAdditionRequest request, CancellationToken ct)
        {
            await adminControllerService.AddSocialAsync(request, ct);
            
            return NoContent();
        }
    }
}
