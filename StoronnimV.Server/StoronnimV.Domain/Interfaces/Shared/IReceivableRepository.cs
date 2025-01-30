using StoronnimV.Domain.Entities.Shared;

namespace StoronnimV.Domain.Interfaces.Shared;

public interface IReceivableRepository
{
    Task<object?> GetByIdAsNoTrackingAsync(long id, CancellationToken ct);
    Task<IEnumerable<object>?> GetAllAsync(CancellationToken ct);
}