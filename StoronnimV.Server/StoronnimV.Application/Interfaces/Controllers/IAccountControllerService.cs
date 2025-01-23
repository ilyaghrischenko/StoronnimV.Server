namespace StoronnimV.Application.Interfaces.Controllers;

public interface IAccountControllerService
{
    Task<string> LogInAsync(string login, string password);
}