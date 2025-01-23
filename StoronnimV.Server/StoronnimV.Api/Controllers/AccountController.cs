using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoronnimV.Application.DTO.Requests.Account;
using StoronnimV.Application.Interfaces.Controllers;

namespace StoronnimV.Api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController(
        IAccountControllerService accountControllerService,
        ILogger<AccountController> logger) : ControllerBase
    {
        private readonly IAccountControllerService _accountControllerService = accountControllerService;
        private readonly ILogger<AccountController> _logger = logger;
        
        [HttpPost("login")]
        public async Task<ActionResult<string>> LogIn([FromBody] LogInRequest request)
        {
            _logger.LogInformation($"Controller: AccountController Method: LogIn with [login: {request.Login}, password: {request.Password}] started at {DateTime.UtcNow}");
            
            var token = await _accountControllerService.LogInAsync(request);
            
            _logger.LogInformation($"Controller: AccountController Method: LogIn with [login: {request.Login}, password: {request.Password}] ended at {DateTime.UtcNow}");
            
            return Ok(token);
        }
    }
}
