using StoronnimV.Domain.Entities.Shared;
using StoronnimV.Domain.Enums;

namespace StoronnimV.Domain.Entities;

public class Admin : BaseEntity
{
    public string Login { get; set; }
    public string Password { get; set; }
    public AdminType Type { get; set; }
    
    public Admin() {}

    public Admin(string login, string password, AdminType type = AdminType.Basic)
    {
        Login = login;
        Password = password;
        Type = type;
    }
}