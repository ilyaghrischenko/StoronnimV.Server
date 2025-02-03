namespace StoronnimV.Application.Contracts.Entities.Shared;

public interface IReceivableByTitleService<TProjection>
{
    Task<IEnumerable<TProjection>> GetItemsByTitleAsync(string title, CancellationToken ct);
}