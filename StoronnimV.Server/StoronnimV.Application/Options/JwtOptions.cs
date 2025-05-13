using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace StoronnimV.Application.Options;

public class JwtOptions
{
    [Required]
    public required string ISSUER {get; init;}
    
    [Required]
    public required string AUDIENCE {get; init;}
    
    [Required]
    public required string KEY {get; init;}
    
    [Required]
    public required int LIFETIME {get; init;}

    public SymmetricSecurityKey GetKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
    }
}