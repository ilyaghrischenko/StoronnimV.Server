namespace StoronnimV.Application.DTO.Requests.Account;

public class LogInRequest(string login, string password)
{
    public string Login { get; set; } = login;
    public string Password { get; set; } = password;
}