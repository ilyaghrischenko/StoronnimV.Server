using StoronnimV.Application.Contracts.Entities.Shared;
using StoronnimV.Domain.Projections.Schedule;

namespace StoronnimV.Application.Contracts.Entities;

public interface IScheduleService
    : IGetByIdService<ScheduleFullProjection>, IGetAllService<ScheduleShortProjection>
{
    Task UpdateStatusesAsync(CancellationToken ct);
}