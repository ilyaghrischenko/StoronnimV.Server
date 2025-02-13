using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace StoronnimV.Application.AutentificationOptions;

public class JwtOptions
{
    public required string ISSUER {get; init;} = string.Empty;
    public required string AUDIENCE {get; init;} = string.Empty;
    public required string KEY {get; init;} = string.Empty;
    public required int LIFETIME {get; init;} = 1;

    public SymmetricSecurityKey GetKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
    }
}