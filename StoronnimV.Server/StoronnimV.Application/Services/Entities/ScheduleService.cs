using Microsoft.Extensions.Logging;
using StoronnimV.Application.Contracts.Entities;
using StoronnimV.Application.Exceptions;
using StoronnimV.Domain.Contracts;
using StoronnimV.Domain.Contracts.Database;
using StoronnimV.Domain.Enums;
using StoronnimV.Domain.Projections.Schedule;

namespace StoronnimV.Application.Services.Entities;

/// <summary>
/// Сервис для проверки полученных данных, полученых с репозитория
/// </summary>
/// <param name="scheduleRepository"></param>
public class ScheduleService(IScheduleRepository scheduleRepository) : IScheduleService
{
    private readonly IScheduleRepository _scheduleRepository = scheduleRepository;
    
    public async Task<ScheduleFullProjection> GetItemByIdAsync(long id, CancellationToken ct)
    {
        ScheduleFullProjection schedule = await _scheduleRepository.GetByIdAsNoTrackingAsync(id, ct)
                                          ?? throw new EntityNotFoundException($"Schedule with id: {id} was not found");
        
        return schedule;
    }

    public async Task<IEnumerable<ScheduleShortProjection>> GetAllAsync(CancellationToken ct)
    {
        var allSchedules = await _scheduleRepository.GetAllAsNoTrackingAsync(ct);
        if (allSchedules is null)
        {
            return new List<ScheduleShortProjection>();
        }

        var result = allSchedules.ToList();
        
        return result;
    }

    public async Task UpdateStatusesAsync(CancellationToken ct)
    {
        var allSchedules = await _scheduleRepository
            .GetAllSchedulesAsync(ct);
        
        DateTime today = DateTime.UtcNow.Date;
        
        var schedulesToChange = allSchedules
            .Where(schedule =>schedule.Status == ScheduleStatus.Active
            && schedule.PerformanceDateTime < today)
            .ToList();
        
        var updateTasks = schedulesToChange.Select(schedule =>
            _scheduleRepository.UpdateAsync(schedule, () =>
            {
                schedule.Status = ScheduleStatus.Passed;
            }, ct)
        );
        
        await Task.WhenAll(updateTasks);
    }
}