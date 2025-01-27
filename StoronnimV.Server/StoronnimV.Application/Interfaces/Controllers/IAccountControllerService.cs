using StoronnimV.Application.DTO.Requests.Account;

namespace StoronnimV.Application.Interfaces.Controllers;

public interface IAccountControllerService
{
    Task<string> LogInAsync(LogInRequest request, CancellationToken ct);
}