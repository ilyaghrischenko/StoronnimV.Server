using StoronnimV.Domain.Contracts.Shared;
using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Projections;

namespace StoronnimV.Domain.Contracts;

public interface IAdminRepository
    : IRepository<Admin>, IGetByIdRepository<AdminProjection>, IGetAllRepository<AdminProjection>
{
    Task<Admin?> GetByLoginAsync(string login, CancellationToken ct);
}