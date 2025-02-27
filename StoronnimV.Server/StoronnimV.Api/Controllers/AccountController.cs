using Microsoft.AspNetCore.Http;
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
        public async Task<ActionResult<string>> LogIn([FromBody] LogInRequest request, CancellationToken ct)
        {
            string token = await accountControllerService.LogInAsync(request, ct);
            
            return Ok(token);
        }
    }
}
