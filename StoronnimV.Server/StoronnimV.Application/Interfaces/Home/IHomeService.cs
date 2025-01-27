namespace StoronnimV.Application.Interfaces.Home;

public interface IHomeService
{
    Task<IEnumerable<object>> GetNewsForHomePageAsync(int count, CancellationToken ct);
    Task<object?> GetScheduleForHomePageAsync(CancellationToken ct);
    Task<object?> GetPromotionVideoForHomePageAsync(CancellationToken ct);
}