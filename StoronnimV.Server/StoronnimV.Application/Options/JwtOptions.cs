using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace StoronnimV.Application.Options;

public class JwtOptions
{
    [Required]
    public string ISSUER {get; init;}
    
    [Required]
    public string AUDIENCE {get; init;}
    
    [Required]
    public string KEY {get; init;}
    
    [Required]
    public int LIFETIME {get; init;}

    public SymmetricSecurityKey GetKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
    }
}