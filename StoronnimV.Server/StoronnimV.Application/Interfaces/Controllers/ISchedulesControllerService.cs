using StoronnimV.Application.DTO.Responses.SchedulePage;

namespace StoronnimV.Application.Interfaces.Controllers;

public interface ISchedulesControllerService
{
    Task<ScheduleResponse> GetItemByIdAsync(long id, CancellationToken ct);
    Task<IEnumerable<ScheduleShortResponse>> GetAllAsync(CancellationToken ct);
}