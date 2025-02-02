using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Interfaces.Shared;
using StoronnimV.Domain.Projections.Schedule;

namespace StoronnimV.Domain.Interfaces;

public interface IScheduleRepository
    : IRepository<Schedule>, IGetByIdRepository<ScheduleFullProjection>, IGetAllRepository<ScheduleFullProjection>
{
    Task<IEnumerable<Schedule>?> GetAllSchedulesAsync(CancellationToken ct);
    Task<object?> GetNearestScheduleForHomePageAsync(CancellationToken ct);
}