using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace StoronnimV.Application.AutentificationOptions;

public class JwtOptions
{
    public required string ISSUER = string.Empty;
    public required string AUDIENCE = string.Empty;
    public required string KEY = string.Empty;
    public required int LIFETIME = 1;

    public SymmetricSecurityKey GetKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
    }
}