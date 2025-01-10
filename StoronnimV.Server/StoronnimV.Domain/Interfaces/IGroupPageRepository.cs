using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Interfaces.Shared;

namespace StoronnimV.Domain.Interfaces;

public interface IGroupPageRepository
    : IRepository<GroupPage>, IReceivableRepository<GroupPage>
{
    public Task<object?> GetFirstGroupPageAsync();
}