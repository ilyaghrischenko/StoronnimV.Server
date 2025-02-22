using System.Security.Claims;
using Microsoft.Extensions.Logging;
using StoronnimV.Application.Contracts.Controllers;
using StoronnimV.Application.Contracts.Entities;
using StoronnimV.Application.Contracts.Jwt;
using StoronnimV.Application.DTO.Requests.Account;
using StoronnimV.Domain.Entities;

namespace StoronnimV.Application.Services.Controllers;

public class AccountControllerService(
    IAdminService adminService,
    IJwtBearerService jwtBearerService) : IAccountControllerService
{
    private readonly IAdminService _adminService = adminService;
    private readonly IJwtBearerService _jwtBearerService = jwtBearerService;

    public async Task<string> LogInAsync(LogInRequest request, CancellationToken ct)
    {
        Admin admin = await _adminService.LogInAsync(request, ct);
        
        ClaimsIdentity identity = _jwtBearerService.GetIdentity(admin);
        string token = _jwtBearerService.GetToken(identity);

        return token;
    }
}