using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StoronnimV.Application.AutentificationOptions;
using StoronnimV.Application.Contracts.Identity;
using StoronnimV.Domain.Entities;

namespace StoronnimV.Application.Services.Identity;

public class JwtBearerService(IOptions<JwtOptions> jwtOptions) : IJwtBearerService
{
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;
    
    public ClaimsIdentity GetIdentity(Admin admin)
    {
        var claims = new List<Claim>
        {
            new(ClaimsIdentity.DefaultNameClaimType, admin.Id.ToString()),
            new(ClaimsIdentity.DefaultRoleClaimType, admin.Type.ToString())
        };
        
        ClaimsIdentity identity = new(claims, "Token",
            ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        
        return identity;
    }

    public string GetToken(ClaimsIdentity identity)
    {
        DateTime timeNow = DateTime.UtcNow;
        JwtSecurityToken jwt = new JwtSecurityToken(
            issuer: _jwtOptions.ISSUER,
            audience: _jwtOptions.AUDIENCE,
            notBefore: timeNow,
            claims: identity.Claims,
            expires: timeNow.Add(TimeSpan.FromDays(_jwtOptions.LIFETIME)),
            signingCredentials: new SigningCredentials(_jwtOptions.GetKey(), SecurityAlgorithms.HmacSha256)
        );
        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    public void SetTokenCookie(HttpResponse response, string token)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,     // Означает, что cookie доступен только через HTTP(S)
            Secure = true,       // Означает, что cookie будет передаваться только через HTTPS
            SameSite = SameSiteMode.Lax, // Защита от CSRF атак
            Expires = DateTime.UtcNow.AddHours(2) // Время жизни cookie (например, 2 часа)
        };

        response.Cookies.Append("Token", token, cookieOptions);
    }
}