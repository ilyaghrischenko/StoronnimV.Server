namespace StoronnimV.Application.Interfaces.Home;

public interface IHomeService
{
    Task<IEnumerable<object>> GetNewsForHomePageAsync(int count);
    Task<object?> GetScheduleForHomePageAsync();
}