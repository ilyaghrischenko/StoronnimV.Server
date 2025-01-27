using StoronnimV.Application.DTO.Requests.Account;
using StoronnimV.Application.Interfaces.Entities.Shared;
using StoronnimV.Domain.Entities;

namespace StoronnimV.Application.Interfaces.Entities;

public interface IAdminService : IReceivableService
{
    public Task<Admin> LogInAsync(LogInRequest request, CancellationToken ct);
}