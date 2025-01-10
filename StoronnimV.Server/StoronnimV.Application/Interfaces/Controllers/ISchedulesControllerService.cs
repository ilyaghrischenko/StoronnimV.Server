using StoronnimV.Application.DTO.Responses.SchedulePage;

namespace StoronnimV.Application.Interfaces.Controllers;

public interface ISchedulesControllerService
{
    Task<ScheduleResponse> GetItemByIdAsync(long id);
    Task<IEnumerable<ScheduleShortResponse>> GetAllAsync();
}