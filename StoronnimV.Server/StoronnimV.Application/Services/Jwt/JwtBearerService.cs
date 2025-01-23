using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using StoronnimV.Application.AutentificationOptions;
using StoronnimV.Application.Interfaces.Jwt;
using StoronnimV.Domain.Entities;

namespace StoronnimV.Application.Services.Jwt;

public class JwtBearerService : IJwtBearerService
{
    public ClaimsIdentity GetIdentity(Admin admin)
    {
        var claims = new List<Claim>
        {
            new(ClaimsIdentity.DefaultNameClaimType, admin.Id.ToString()),
            new(ClaimsIdentity.DefaultRoleClaimType, admin.Type.ToString())
        };
        
        var identity = new ClaimsIdentity(claims, "Token",
            ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        
        return identity;
    }

    public string GetToken(ClaimsIdentity identity)
    {
        var timeNow = DateTime.UtcNow;
        var jwt = new JwtSecurityToken(
            issuer: JwtOptions.ISSUER,
            audience: JwtOptions.AUDIENCE,
            notBefore: timeNow,
            claims: identity.Claims,
            expires: timeNow.Add(TimeSpan.FromDays(JwtOptions.LIFETIME)),
            signingCredentials: new SigningCredentials(JwtOptions.GetKey(), SecurityAlgorithms.HmacSha256)
        );
        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}