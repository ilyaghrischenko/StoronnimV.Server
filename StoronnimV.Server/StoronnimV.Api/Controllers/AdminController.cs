using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoronnimV.Application.DTO.Responses.NewsPage;
using StoronnimV.Application.DTO.Responses.Shared;
using StoronnimV.Application.DTO.Responses.Video;
using StoronnimV.Application.Interfaces.Controllers;

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
        
        [HttpGet("news/page/{page:int}")]
        public async Task<ActionResult<PaginationResponse<NewsResponse>>> GetNewsForAdminPage([FromRoute] int page,
            CancellationToken ct, [FromQuery] int pageSize = 30)
        {
            _logger.LogInformation($"Controller: AdminController Method: GetNewsForAdminPage with [page: {page}, pageSize: {pageSize}] started at {DateTime.UtcNow}");
            
            var newsPaginationResponse = await _adminControllerService.GetNewsForPageAsync(page, pageSize, ct);
            
            _logger.LogInformation($"Controller: AdminController Method: GetNewsForAdminPage with [page: {page}, pageSize: {pageSize}] ended at {DateTime.UtcNow}");

            return Ok(newsPaginationResponse);
        }

        [HttpGet("videos/page/{page:int}")]
        public async Task<ActionResult<PaginationResponse<VideoPageResponse>>> GetVideosForAdminPage([FromRoute] int page,
            CancellationToken ct, [FromQuery] int pageSize = 30)
        {
            _logger.LogInformation($"Controller: AdminController Method: GetVideosForAdminPage with [page: {page}, pageSize: {pageSize}] started at {DateTime.UtcNow}");
            
            var videosPaginationResult = await _adminControllerService.GetVideosForPageAsync(page, pageSize, ct);
            
            _logger.LogInformation($"Controller: AdminController Method: GetVideosForAdminPage with [page: {page}, pageSize: {pageSize}] ended at {DateTime.UtcNow}");

            return Ok(videosPaginationResult);
        }
    }
}
