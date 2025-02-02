using Microsoft.Extensions.Logging;
using StoronnimV.Application.Exceptions;
using StoronnimV.Application.Extensions;
using StoronnimV.Application.Interfaces.Entities;
using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Enums;
using StoronnimV.Domain.Interfaces;
using StoronnimV.Domain.Projections.Schedule;

namespace StoronnimV.Application.Services.Entities;

/// <summary>
/// Сервис для проверки полученных данных, полученых с репозитория
/// </summary>
/// <param name="scheduleRepository"></param>
public class ScheduleService(IScheduleRepository scheduleRepository,
    ILogger<ScheduleService> logger) : IScheduleService
{
    private readonly IScheduleRepository _scheduleRepository = scheduleRepository;
    private readonly ILogger<ScheduleService> _logger = logger;
    
    public async Task<ScheduleFullProjection> GetItemByIdAsync(long id, CancellationToken ct)
    {
        _logger.LogInformation($"Service: ScheduleService Method: GetItemByIdAsync with id: {id} started at {DateTime.UtcNow}");
        
        ScheduleFullProjection schedule = await _scheduleRepository.GetByIdAsNoTrackingAsync(id, ct)
                                          ?? throw new EntityNotFoundException($"Schedule with id: {id} was not found");
        
        _logger.LogInformation($"Service: ScheduleService Method: GetItemByIdAsync with id: {id} ended at {DateTime.UtcNow}");

        return schedule;
    }

    public async Task<IEnumerable<ScheduleFullProjection>> GetAllAsync(CancellationToken ct)
    {
        _logger.LogInformation($"Service: ScheduleService Method: GetAllAsync started at {DateTime.UtcNow}");
        
        var allSchedules = await _scheduleRepository.GetAllAsNoTrackingAsync(ct);
        if (allSchedules is null)
        {
            return new List<ScheduleFullProjection>();
        }

        var result = allSchedules
            .Where(schedule => (string)schedule.GetPropertyValue("Status")! == "Active")
            .ToList();
        
        _logger.LogInformation($"Service: ScheduleService Method: GetAllAsync ended at {DateTime.UtcNow}");

        return result;
    }

    public async Task UpdateStatusesAsync(CancellationToken ct)
    {
        _logger.LogInformation($"Service: ScheduleService Method: UpdateStatusesAsync started at {DateTime.UtcNow}");
        
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
        
        _logger.LogInformation($"Service: ScheduleService Method: UpdateStatusesAsync ended at {DateTime.UtcNow}");
    }
}