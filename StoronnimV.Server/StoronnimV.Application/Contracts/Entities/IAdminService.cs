using StoronnimV.Application.Contracts.Entities.Shared;
using StoronnimV.Application.DTO.Requests.Account;
using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Projections;

namespace StoronnimV.Application.Contracts.Entities;

public interface IAdminService : IGetByIdService<AdminProjection>, IGetAllService<AdminProjection>
{
    public Task<Admin> LogInAsync(LogInRequest request, CancellationToken ct);
}