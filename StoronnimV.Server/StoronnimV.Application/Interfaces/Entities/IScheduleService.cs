using StoronnimV.Application.Interfaces.Entities.Shared;
using StoronnimV.Domain.Projections.Schedule;

namespace StoronnimV.Application.Interfaces.Entities;

public interface IScheduleService
    : IGetByIdService<ScheduleFullProjection>, IGetAllService<ScheduleShortProjection>
{
    Task UpdateStatusesAsync(CancellationToken ct);
}