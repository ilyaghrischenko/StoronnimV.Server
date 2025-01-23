using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Interfaces.Shared;

namespace StoronnimV.Domain.Interfaces;

public interface IAdminRepository
    : IRepository<Admin>, IReceivableRepository<Admin>
{
    Task<Admin?> GetByLoginAsync(string login);
}