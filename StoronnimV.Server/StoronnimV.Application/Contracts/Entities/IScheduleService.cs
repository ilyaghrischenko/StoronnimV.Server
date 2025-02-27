using StoronnimV.Application.Contracts.Entities.Shared;
using StoronnimV.Domain.Projections.Schedule;

namespace StoronnimV.Application.Contracts.Entities;

public interface IScheduleService
    : IGetByIdService<ScheduleFullProjection>, IPaginationService<ScheduleShortProjection>
{
    Task UpdateStatusesAsync(CancellationToken ct);
}