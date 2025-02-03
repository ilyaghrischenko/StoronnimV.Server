namespace StoronnimV.Domain.Contracts.Shared;

public interface IReceivableByTitleRepository<TProjection>
{
    Task<IEnumerable<TProjection>?> GetItemsByTitle(string title, CancellationToken ct);
}