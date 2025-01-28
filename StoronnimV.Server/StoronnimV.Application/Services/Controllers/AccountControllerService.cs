using System.Security.Claims;
using Microsoft.Extensions.Logging;
using StoronnimV.Application.DTO.Requests.Account;
using StoronnimV.Application.Interfaces.Controllers;
using StoronnimV.Application.Interfaces.Entities;
using StoronnimV.Application.Interfaces.Jwt;
using StoronnimV.Domain.Entities;

namespace StoronnimV.Application.Services.Controllers;

public class AccountControllerService(
    IAdminService adminService,
    IJwtBearerService jwtBearerService,
    ILogger<AccountControllerService> logger) : IAccountControllerService
{
    private readonly IAdminService _adminService = adminService;
    private readonly IJwtBearerService _jwtBearerService = jwtBearerService;
    private readonly ILogger<AccountControllerService> _logger = logger;

    public async Task<string> LogInAsync(LogInRequest request, CancellationToken ct)
    {
        _logger.LogInformation($"Service: AccountControllerService Method: LogInAsync with [login: {request.Login}, password: {request.Password}] started at {DateTime.UtcNow}");

        Admin admin = await _adminService.LogInAsync(request, ct);
        
        ClaimsIdentity identity = _jwtBearerService.GetIdentity(admin);
        string token = _jwtBearerService.GetToken(identity);
        
        _logger.LogInformation($"Service: AccountControllerService Method: LogInAsync with [login: {request.Login}, password: {request.Password}] ended at {DateTime.UtcNow}");

        return token;
    }
}