using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoronnimV.Application.DTO.Requests.Account;

namespace StoronnimV.Api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public async Task<ActionResult<string>> LogIn([FromBody] LogInRequest request)
        {
            //TODO
            return Ok();
        }
    }
}
