using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoronnimV.Application.Contracts.Controllers;

namespace StoronnimV.Api.Controllers
{
    /// <summary>
    /// Контроллер для админа, он позволяет управлять данными, которые отображаются на страницых (Удалять, изменять)
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

        [HttpDelete("news/{id:long}")]
        public async Task<IActionResult> DeleteNewsItem([FromRoute] long id, CancellationToken ct)
        {
            _logger.LogInformation($"Controller: AdminController Method: DeleteNewsItem with id: {id} started at {DateTime.UtcNow}");

            await _adminControllerService.DeleteNewsItemAsync(id, ct);
            
            _logger.LogInformation($"Controller: AdminController Method: DeleteNewsItem with id: {id} ended at {DateTime.UtcNow}");

            return NoContent();
        }

        [HttpDelete("schedules/{id:long}")]
        public async Task<IActionResult> DeleteSchedule([FromRoute] long id, CancellationToken ct)
        {
            _logger.LogInformation($"Controller: AdminController Method: DeleteSchedule with id: {id} started at {DateTime.UtcNow}");
            
            await _adminControllerService.DeleteScheduleAsync(id, ct);
            
            _logger.LogInformation($"Controller: AdminController Method: DeleteSchedule with id: {id} ended at {DateTime.UtcNow}");

            return NoContent();
        }

        [HttpDelete("videos/{id:long}")]
        public async Task<IActionResult> DeleteVideo([FromRoute] long id, CancellationToken ct)
        {
            _logger.LogInformation($"Controller: AdminController Method: DeleteVideo with id: {id} started at {DateTime.UtcNow}");
            
            await _adminControllerService.DeleteVideoAsync(id, ct);
            
            _logger.LogInformation($"Controller: AdminController Method: DeleteVideo with id: {id} ended at {DateTime.UtcNow}");
            
            return NoContent();
        }

        [HttpDelete("group/{id:long}")]
        public async Task<IActionResult> DeleteGroup([FromRoute] long id, CancellationToken ct)
        {
            _logger.LogInformation($"Controller: AdminController Method: DeleteGroup with id: {id} started at {DateTime.UtcNow}");

            await _adminControllerService.DeleteGroupPageAsync(id, ct);
            
            _logger.LogInformation($"Controller: AdminController Method: DeleteGroup with id: {id} ended at {DateTime.UtcNow}");

            return NoContent();
        }

        [HttpDelete("group/members/{id:long}")]
        public async Task<IActionResult> DeleteMember([FromRoute] long id, CancellationToken ct)
        {
            _logger.LogInformation($"Controller: AdminController Method: DeleteMember with id: {id} started at {DateTime.UtcNow}");
            
            await _adminControllerService.DeleteMemberAsync(id, ct);
            
            _logger.LogInformation($"Controller: AdminController Method: DeleteMember with id: {id} ended at {DateTime.UtcNow}");

            return NoContent();
        }

        [HttpDelete("music/{id:long}")]
        public async Task<IActionResult> DeleteMusicPlatform([FromRoute] long id, CancellationToken ct)
        {
            _logger.LogInformation($"Controller: AdminController Method: DeleteMusicPlatform with id: {id} started at {DateTime.UtcNow}");
            
            await _adminControllerService.DeleteMusicPlatformAsync(id, ct);
            
            _logger.LogInformation($"Controller: AdminController Method: DeleteMusicPlatform with id: {id} ended at {DateTime.UtcNow}");

            return NoContent();
        }

        [HttpDelete("socials/{id:long}")]
        public async Task<IActionResult> DeleteSocial([FromRoute] long id, CancellationToken ct)
        {
            _logger.LogInformation($"Controller: AdminController Method: DeleteSocial with id: {id} started at {DateTime.UtcNow}");
            
            await _adminControllerService.DeleteSocialAsync(id, ct);
            
            _logger.LogInformation($"Controller: AdminController Method: DeleteSocial with id: {id} ended at {DateTime.UtcNow}");

            return NoContent();
        }
    }
}
