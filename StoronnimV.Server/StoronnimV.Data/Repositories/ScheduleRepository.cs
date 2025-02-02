using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StoronnimV.Data.Repositories.Shared;
using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Enums;
using StoronnimV.Domain.Interfaces;
using StoronnimV.Domain.Projections.Schedule;

namespace StoronnimV.Data.Repositories;

/// <summary>
/// Репозиторий для получения данных напрямую с бд
/// </summary>
/// <param name="contextFactory"></param>
public class ScheduleRepository(
    IDbContextFactory<StoronnimVContext> contextFactory,
    ILogger<ScheduleRepository> logger)
    : Repository<Schedule>(contextFactory), IScheduleRepository
{
    private readonly IDbContextFactory<StoronnimVContext> _contextFactory = contextFactory;
    private readonly ILogger<ScheduleRepository> _logger = logger;

    public async Task<ScheduleFullProjection?> GetByIdAsNoTrackingAsync(long id, CancellationToken ct)
    {
        _logger.LogInformation(
            $"Repository: ScheduleRepository Method: GetByIdAsNoTrackingAsync with id: {id} started at {DateTime.UtcNow}");

        await using StoronnimVContext context = await _contextFactory.CreateDbContextAsync(ct);
        var dbSet = context.Schedules;
        var query = ApplyIncludes(dbSet);

        ScheduleFullProjection? result = await query
            .AsNoTracking()
            .Select(schedule => new ScheduleFullProjection
            {
                Id = schedule.Id,
                Photo = schedule.Photo,
                Title = schedule.Title,
                Description = schedule.Description,
                PerformanceDateTime = schedule.PerformanceDateTime,
                Location = schedule.Location,
                Status = schedule.Status
            })
            .FirstOrDefaultAsync(schedule => schedule.Id == id, ct);

        _logger.LogInformation(
            $"Repository: ScheduleRepository Method: GetByIdAsNoTrackingAsync with id: {id} ended at {DateTime.UtcNow}");

        return result;
    }

    public async Task<IEnumerable<ScheduleFullProjection>?> GetAllAsNoTrackingAsync(CancellationToken ct)
    {
        _logger.LogInformation($"Repository: ScheduleRepository Method: GetAllAsync started at {DateTime.UtcNow}");

        await using StoronnimVContext context = await _contextFactory.CreateDbContextAsync(ct);
        var dbSet = context.Schedules;
        var query = ApplyIncludes(dbSet);

        var result = await query
            .AsNoTracking()
            .Select(schedule => new ScheduleFullProjection
            {
                Id = schedule.Id,
                Photo = schedule.Photo,
                Title = schedule.Title,
                Description = schedule.Description,
                PerformanceDateTime = schedule.PerformanceDateTime,
                Location = schedule.Location,
                Status = schedule.Status
            })
            .ToListAsync(ct);

        _logger.LogInformation($"Repository: ScheduleRepository Method: GetAllAsync ended at {DateTime.UtcNow}");

        return result;
    }

    public async Task<IEnumerable<Schedule>?> GetAllSchedulesAsync(CancellationToken ct)
    {
        _logger.LogInformation(
            $"Repository: ScheduleRepository Method: GetAllSchedulesAsync started at {DateTime.UtcNow}");

        await using StoronnimVContext context = await _contextFactory.CreateDbContextAsync(ct);
        var dbSet = context.Schedules;
        var query = ApplyIncludes(dbSet);

        var result = await dbSet.ToListAsync(ct);

        _logger.LogInformation(
            $"Repository: ScheduleRepository Method: GetAllSchedulesAsync ended at {DateTime.UtcNow}");

        return result;
    }

    public async Task<object?> GetNearestScheduleForHomePageAsync(CancellationToken ct)
    {
        _logger.LogInformation(
            $"Repository: ScheduleRepository Method: GetScheduleForHomePageAsync started at {DateTime.UtcNow}");

        await using StoronnimVContext context = await _contextFactory.CreateDbContextAsync(ct);
        var dbSet = context.Schedules;
        var query = ApplyIncludes(dbSet);

        ScheduleShortProjection? result = await query
            .AsNoTracking()
            .Where(schedule => schedule.Status == ScheduleStatus.Active)
            .OrderBy(schedule => schedule.PerformanceDateTime)
            .Select(schedule => new ScheduleShortProjection
            {
                Id = schedule.Id,
                Photo = schedule.Photo,
                Title = schedule.Title,
                Description = schedule.Description,
                PerformanceDateTime = schedule.PerformanceDateTime,
                Location = schedule.Location
            })
            .FirstOrDefaultAsync(ct);

        _logger.LogInformation(
            $"Repository: ScheduleRepository Method: GetScheduleForHomePageAsync ended at {DateTime.UtcNow}");

        return result;
    }
}