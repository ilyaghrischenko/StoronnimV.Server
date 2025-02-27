using Microsoft.AspNetCore.Http;
using StoronnimV.Application.DTO.Requests.Account;

namespace StoronnimV.Application.Contracts.Controllers;

public interface IAccountControllerService
{
    Task LogInAsync(HttpResponse response, LogInRequest request, CancellationToken ct);
}