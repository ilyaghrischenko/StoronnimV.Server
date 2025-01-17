using StoronnimV.Application.DTO.Responses.HomePage;
using StoronnimV.Application.DTO.Responses.Video;

namespace StoronnimV.Application.Interfaces.Controllers;

public interface IHomeControllerService
{
    Task<IEnumerable<NewsHomeResponse>> GetNewsAsync(int count);
    Task<ScheduleHomeResponse> GetScheduleAsync();
    Task<VideoPageResponse> GetVideoAsync();
}