using StoronnimV.Application.Interfaces.Entities.Shared;

namespace StoronnimV.Application.Interfaces.Entities;

public interface ISocialService : IReceivableService
{
    public Task<IEnumerable<object>> GetAllForMemberAsync(long memberId);
}