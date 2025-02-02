using StoronnimV.Application.DTO.Requests.Account;

namespace StoronnimV.Application.Contracts.Controllers;

public interface IAccountControllerService
{
    Task<string> LogInAsync(LogInRequest request, CancellationToken ct);
}