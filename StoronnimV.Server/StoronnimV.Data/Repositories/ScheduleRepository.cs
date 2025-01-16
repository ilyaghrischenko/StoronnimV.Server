using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StoronnimV.Data.Repositories.Shared;
using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Enums;
using StoronnimV.Domain.Interfaces;

namespace StoronnimV.Data.Repositories;

/// <summary>
/// Репозиторий для получения данных напрямую с бд
/// </summary>
/// <param name="contextFactory"></param>
public class ScheduleRepository(IDbContextFactory<StoronnimVContext> contextFactory,
    ILogger<ScheduleRepository> logger)
    : Repository<Schedule>(contextFactory), IScheduleRepository
{
    private readonly IDbContextFactory<StoronnimVContext> _contextFactory = contextFactory;
    private readonly ILogger<ScheduleRepository> _logger = logger;
    
    public async Task<object?> GetByIdAsNoTrackingAsync(long id)
    {
        _logger.LogInformation($"Repository: ScheduleRepository Method: GetByIdAsNoTrackingAsync with id: {id} started at {DateTime.UtcNow}");
        
        using var context = await _contextFactory.CreateDbContextAsync();
        var dbSet = context.Schedules;
        var query = ApplyIncludes(dbSet);
        
        _logger.LogInformation($"Repository: ScheduleRepository Method: GetByIdAsNoTrackingAsync with id: {id} ended at {DateTime.UtcNow}");

        return await query
            .AsNoTracking()
            .Select(schedule => new
            {
                Id = schedule.Id,
                Photo = schedule.Photo,
                Title = schedule.Title,
                Description = schedule.Description,
                PerformanceDateTime = schedule.PerformanceDateTime.ToShortDateString(),
                Location = schedule.Location,
                Status = schedule.Status.ToString()
            })
            .FirstOrDefaultAsync(schedule => schedule.Id == id);
    }

    public async Task<IEnumerable<object>?> GetAllAsync()
    {
        _logger.LogInformation($"Repository: ScheduleRepository Method: GetAllAsync started at {DateTime.UtcNow}");
        
        using var context = await _contextFactory.CreateDbContextAsync();
        var dbSet = context.Schedules;
        var query = ApplyIncludes(dbSet);
        
        _logger.LogInformation($"Repository: ScheduleRepository Method: GetAllAsync ended at {DateTime.UtcNow}");

        return await query
            .AsNoTracking()
            .Select(schedule => new
            {
                Id = schedule.Id,
                Photo = schedule.Photo,
                Title = schedule.Title,
                Description = schedule.Description,
                PerformanceDateTime = schedule.PerformanceDateTime.ToShortDateString(),
                Location = schedule.Location,
                Status = schedule.Status.ToString()
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<Schedule>?> GetAllSchedulesAsync()
    {
        _logger.LogInformation($"Repository: ScheduleRepository Method: GetAllSchedulesAsync started at {DateTime.UtcNow}");
        
        using var context = await _contextFactory.CreateDbContextAsync();
        var dbSet = context.Schedules;
        var query = ApplyIncludes(dbSet);

        _logger.LogInformation($"Repository: ScheduleRepository Method: GetAllSchedulesAsync ended at {DateTime.UtcNow}");

        return await dbSet.ToListAsync();
    }

    public async Task<object?> GetScheduleForHomePageAsync()
    {
        _logger.LogInformation($"Repository: ScheduleRepository Method: GetScheduleForHomePageAsync started at {DateTime.UtcNow}");
        
        using var context = await _contextFactory.CreateDbContextAsync();
        var dbSet = context.Schedules;
        var query = ApplyIncludes(dbSet);
        
        _logger.LogInformation($"Repository: ScheduleRepository Method: GetScheduleForHomePageAsync ended at {DateTime.UtcNow}");
        
        return await query
            .AsNoTracking()
            .Where(schedule => schedule.Status == ScheduleStatus.Active)
            .OrderBy(schedule => schedule.PerformanceDateTime)
            .Select(schedule => new
            {
                Id = schedule.Id,
                Photo = schedule.Photo,
                Title = schedule.Title,
                Description = schedule.Description,
                PerformanceDateTime = schedule.PerformanceDateTime.ToShortDateString(),
                Location = schedule.Location
            })
            .FirstOrDefaultAsync();
    }
}