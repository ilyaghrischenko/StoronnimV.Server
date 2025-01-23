using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace StoronnimV.Application.AutentificationOptions;

public class JwtOptions
{
    public const string ISSUER = "MyIssuer";
    public const string AUDIENCE = "MyAudience";
    const string KEY = "eiogjoeirjgporkfdkfska[fkpowejtirugriwut8024ur902jfoekgjoejoirgjeohigoershgjldfhnlgj,o.p/asdfgafdgadfgdfgadfbadfgafdvbsfgbaf";
    public const int LIFETIME = 1;

    public static SymmetricSecurityKey GetKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
    }
}