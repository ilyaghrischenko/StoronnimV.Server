using StoronnimV.Application.Interfaces.Entities.Shared;

namespace StoronnimV.Application.Interfaces.Entities;

public interface IScheduleService : IReceivableService
{
    Task UpdateStatusesAsync(CancellationToken ct);
}