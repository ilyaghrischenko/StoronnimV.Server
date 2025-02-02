using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Interfaces.Shared;
using StoronnimV.Domain.Projections;

namespace StoronnimV.Domain.Interfaces;

public interface IAdminRepository
    : IRepository<Admin>, IGetByIdRepository<AdminProjection>, IGetAllRepository<AdminProjection>
{
    Task<Admin?> GetByLoginAsync(string login, CancellationToken ct);
}