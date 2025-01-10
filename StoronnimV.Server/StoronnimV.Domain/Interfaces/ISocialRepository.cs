using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Interfaces.Shared;

namespace StoronnimV.Domain.Interfaces;

public interface ISocialRepository
    : IRepository<Social>, IReceivableRepository<Social>
{
    public Task<IEnumerable<object>?> GetAllForMemberAsync(long memberId);
}