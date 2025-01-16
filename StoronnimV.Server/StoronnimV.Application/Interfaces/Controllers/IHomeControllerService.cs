using StoronnimV.Application.DTO.Responses.HomePage;

namespace StoronnimV.Application.Interfaces.Controllers;

public interface IHomeControllerService
{
    Task<IEnumerable<NewsHomeResponse>> GetNewsAsync(int count);
    Task<ScheduleHomeResponse> GetScheduleAsync();
}