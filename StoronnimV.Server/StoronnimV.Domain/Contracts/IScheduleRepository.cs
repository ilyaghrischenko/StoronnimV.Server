using StoronnimV.Domain.Contracts.Shared;
using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Projections.Schedule;

namespace StoronnimV.Domain.Contracts;

public interface IScheduleRepository
    : IRepository<Schedule>, IGetByIdRepository<ScheduleFullProjection>, IGetAllRepository<ScheduleShortProjection>
{
    Task<IEnumerable<Schedule>?> GetAllSchedulesAsync(CancellationToken ct);
    Task<ScheduleShortProjection?> GetNearestScheduleForHomePageAsync(CancellationToken ct);
}