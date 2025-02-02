using StoronnimV.Application.DTO.Requests.Account;
using StoronnimV.Application.Interfaces.Entities.Shared;
using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Projections;

namespace StoronnimV.Application.Interfaces.Entities;

public interface IAdminService : IGetByIdService<AdminProjection>, IGetAllService<AdminProjection>
{
    public Task<Admin> LogInAsync(LogInRequest request, CancellationToken ct);
}