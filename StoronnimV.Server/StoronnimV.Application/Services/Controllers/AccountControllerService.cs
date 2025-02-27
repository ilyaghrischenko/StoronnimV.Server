using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using StoronnimV.Application.Contracts.Controllers;
using StoronnimV.Application.Contracts.Entities;
using StoronnimV.Application.Contracts.Identity;
using StoronnimV.Application.DTO.Requests.Account;
using StoronnimV.Domain.Entities;

namespace StoronnimV.Application.Services.Controllers;

public class AccountControllerService(
    IAccountService accountService,
    IJwtBearerService jwtBearerService) : IAccountControllerService
{
    public async Task LogInAsync(HttpResponse response, LogInRequest request, CancellationToken ct)
    {
        Admin admin = await accountService.LogInAsync(request.Login, request.Password, ct);
        
        ClaimsIdentity identity = jwtBearerService.GetIdentity(admin);
        string token = jwtBearerService.GetToken(identity);
        jwtBearerService.SetTokenCookie(response, token);
    }
}