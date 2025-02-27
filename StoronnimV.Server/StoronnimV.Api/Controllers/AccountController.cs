using Microsoft.AspNetCore.Mvc;
using StoronnimV.Application.Contracts.Controllers;
using StoronnimV.Application.DTO.Requests.Account;

namespace StoronnimV.Api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController(
        IAccountControllerService accountControllerService) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> LogIn([FromBody] LogInRequest request, CancellationToken ct)
        {
            await accountControllerService.LogInAsync(Response, request, ct);
            
            return Ok();
        }
    }
}
