using StoronnimV.Application.Contracts.Entities.Shared;
using StoronnimV.Application.DTO.Requests.Entities.Pages.Addition;
using StoronnimV.Domain.Projections.Schedule;

namespace StoronnimV.Application.Contracts.Entities;

public interface IScheduleService
    : IGetByIdService<ScheduleFullProjection>, IPaginationService<ScheduleShortProjection>
{
    Task UpdateStatusesAsync(CancellationToken ct);
    Task AddScheduleAsync(ScheduleAdditionRequest request, CancellationToken ct);
    Task DeleteScheduleAsync(long id, CancellationToken ct);
    
    //todo: update schedule
    // public async Task UpdateScheduleAsync(ScheduleUpdateRequest request, CancellationToken ct)
}