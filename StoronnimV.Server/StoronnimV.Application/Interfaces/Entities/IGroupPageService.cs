using StoronnimV.Application.Interfaces.Entities.Shared;

namespace StoronnimV.Application.Interfaces.Entities;

public interface IGroupPageService : IReceivableService
{
    public Task<object> GetFirstGroupPageAsync(CancellationToken ct);
}