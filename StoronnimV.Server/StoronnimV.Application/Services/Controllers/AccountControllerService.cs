using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StoronnimV.Application.Contracts.Controllers;
using StoronnimV.Application.Contracts.Entities;
using StoronnimV.Application.Contracts.Identity;
using StoronnimV.Application.DTO.Requests.Account;
using StoronnimV.Application.Options;
using StoronnimV.Domain.Entities;

namespace StoronnimV.Application.Services.Controllers;

public class AccountControllerService(
    IAccountService accountService,
    IJwtBearerService jwtBearerService,
    IOptions<CookieSettings> cookieSettings) : IAccountControllerService
{
    private readonly CookieSettings _cookieSettings = cookieSettings.Value;
    
    public async Task LogInAsync(HttpResponse response, LogInRequest request, CancellationToken ct)
    {
        Admin admin = await accountService.LogInAsync(request.Login, request.Password, ct);
        
        ClaimsIdentity identity = jwtBearerService.GetIdentity(admin);
        string token = jwtBearerService.GetToken(identity);

        var cookieOptions = new CookieOptions
        {
            HttpOnly = _cookieSettings.HttpOnly,
            Secure = _cookieSettings.Secure,
            SameSite = Enum.Parse<SameSiteMode>(_cookieSettings.SameSite),
            Expires = DateTime.UtcNow.AddHours(_cookieSettings.ExpiresInHours)
        };
        
        response.Cookies.Append("Token", token, cookieOptions);
    }
}