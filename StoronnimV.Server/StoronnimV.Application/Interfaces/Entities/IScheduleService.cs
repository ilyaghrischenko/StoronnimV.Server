using StoronnimV.Application.Interfaces.Entities.Shared;
using StoronnimV.Domain.Projections.Schedule;

namespace StoronnimV.Application.Interfaces.Entities;

public interface IScheduleService
    : IGetByIdService<ScheduleFullProjection>, IGetAllService<ScheduleFullProjection>
{
    Task UpdateStatusesAsync(CancellationToken ct);
}