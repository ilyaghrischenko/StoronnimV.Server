using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoronnimV.Application.Contracts.Controllers;
using StoronnimV.Application.DTO.Requests.Entities.Admin;
using StoronnimV.Application.DTO.Responses.Admin;

namespace StoronnimV.Api.Controllers
{
    [Authorize(Policy = "SuperAdminOnly")]
    [Route("api/super-admin/basic-admin")]
    [ApiController]
    public class SuperAdminController(ISuperAdminControllerService superAdminControllerService)
        : ControllerBase
    {
        private readonly ISuperAdminControllerService _superAdminControllerService = superAdminControllerService;
        
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<BasicAdminResponse>>> GetAllBasicAdmins(CancellationToken ct)
        {
            var admins = await _superAdminControllerService.GetAllAsync(ct);

            return Ok(admins);
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteBasicAdmin([FromRoute] long id, CancellationToken ct)
        {
            await _superAdminControllerService.DeleteBasicAdminAsync(id, ct);

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBasicAdmin([FromBody] CreateBasicAdminRequest request, CancellationToken ct)
        {
            await _superAdminControllerService.AddBasicAdminAsync(request, ct);

            return Created();
        }
        
        [HttpPatch("login")]
        public async Task<IActionResult> EditBasicAdminLogin([FromBody] EditBasicAdminLoginRequest loginRequest, CancellationToken ct)
        {
            await _superAdminControllerService.EditBasicAdminLoginAsync(loginRequest, ct);

            return Ok();
        }

        [HttpPatch("password")]
        public async Task<IActionResult> EditBasicAdminPassword([FromBody] EditBasicAdminPasswordRequest passwordRequest, CancellationToken ct)
        {
            await _superAdminControllerService.EditBasicAdminPasswordAsync(passwordRequest, ct);

            return Ok();
        }
    }
}
