using StoronnimV.Domain.Contracts.Database.Shared;
using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Projections;

namespace StoronnimV.Domain.Contracts.Database;

public interface IAdminRepository
    : IRepository<Admin>, IGetByIdRepository<AdminProjection>, IGetAllRepository<AdminProjection>
{
    Task<Admin?> GetByLoginAsync(string login, CancellationToken ct);
}