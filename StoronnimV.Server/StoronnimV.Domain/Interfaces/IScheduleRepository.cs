using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Interfaces.Shared;

namespace StoronnimV.Domain.Interfaces;

public interface IScheduleRepository
    : IRepository<Schedule>, IReceivableRepository<Schedule>
{
    Task<IEnumerable<Schedule>?> GetAllSchedulesAsync();
    Task<object?> GetScheduleForHomePageAsync();
}