using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Interfaces.Shared;

namespace StoronnimV.Domain.Interfaces;

public interface IScheduleRepository
    : IRepository<Schedule>, IReceivableRepository
{
    Task<IEnumerable<Schedule>?> GetAllSchedulesAsync(CancellationToken ct);
    Task<object?> GetNearestScheduleForHomePageAsync(CancellationToken ct);
}