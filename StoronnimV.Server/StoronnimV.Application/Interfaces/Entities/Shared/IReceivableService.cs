namespace StoronnimV.Application.Interfaces.Entities.Shared;

public interface IReceivableService
{
    Task<object> GetItemByIdAsync(long id, CancellationToken ct);
    Task<IEnumerable<object>> GetAllAsync(CancellationToken ct);
}