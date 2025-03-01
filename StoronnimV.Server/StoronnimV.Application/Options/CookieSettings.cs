namespace StoronnimV.Application.Options;

public class CookieSettings
{
    public bool HttpOnly { get; set; }
    public bool Secure { get; set; }
    public string SameSite { get; set; }
    public int ExpiresInHours { get; set; }
}