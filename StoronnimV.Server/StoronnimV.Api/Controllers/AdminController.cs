using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoronnimV.Application.Contracts.Controllers;
using StoronnimV.Application.DTO.Responses.GroupPage;
using StoronnimV.Application.DTO.Responses.NewsPage;
using StoronnimV.Application.DTO.Responses.Shared;
using StoronnimV.Application.DTO.Responses.Video;

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
    }
}
