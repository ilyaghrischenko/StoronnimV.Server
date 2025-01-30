namespace StoronnimV.Application.Interfaces.Home;

public interface IHomeService
{
    Task<IEnumerable<object>> GetMainNewsForHomePageAsync(int count, CancellationToken ct);
    Task<object?> GetNearestScheduleForHomePageAsync(CancellationToken ct);
    Task<object?> GetPromotionVideoForHomePageAsync(CancellationToken ct);
}