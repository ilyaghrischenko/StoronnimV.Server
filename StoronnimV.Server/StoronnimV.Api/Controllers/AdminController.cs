using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoronnimV.Application.Contracts.Controllers;
using StoronnimV.Application.DTO.Responses.Admin;

namespace StoronnimV.Api.Controllers
{
    /// <summary>
    /// Контроллер для админа, он позволяет управлять данными, которые отображаются на страницых (Удалять, изменять),
    /// а так же управлять данными обычного адмниа (логин и пароль)
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/admin")]
    [ApiController]
    public class AdminController(
        IAdminControllerService adminControllerService,
        ILogger<AdminController> logger) : ControllerBase
    {
        private readonly IAdminControllerService _adminControllerService = adminControllerService;
        private readonly ILogger<AdminController> _logger = logger;

        [Authorize(Policy = "SuperAdminOnly")]
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<BasicAdminResponse>>> GetAllBasicAdmins(CancellationToken ct)
        {
            var admins = await _adminControllerService.GetAllBasicAdminsAsync(ct);

            return Ok(admins);
        }

        [Authorize(Policy = "SuperAdminOnly")]
        [HttpDelete("/{id:long}")]
        public async Task<IActionResult> DeleteBasicAdmin([FromRoute] long id, CancellationToken ct)
        {
            await _adminControllerService.DeleteBasicAdminAsync(id, ct);

            return NoContent();
        }

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
    }
}
