using StoronnimV.Domain.Entities.Shared;

namespace StoronnimV.Domain.Interfaces.Shared;

public interface IReceivableRepository<T> where T : BaseEntity
{
    Task<object?> GetByIdAsNoTrackingAsync(long id, CancellationToken ct);
    Task<IEnumerable<object>?> GetAllAsync(CancellationToken ct);
}