using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StoronnimV.Domain.Contracts;
using StoronnimV.Domain.Contracts.Database;
using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Enums;
using StoronnimV.Domain.Projections.Schedule;
using StoronnimV.Infrastructure.Repositories.Database.Shared;

namespace StoronnimV.Infrastructure.Repositories.Database;

/// <summary>
/// Репозиторий для получения данных напрямую с бд
/// </summary>
/// <param name="contextFactory"></param>
public class ScheduleRepository(IDbContextFactory<StoronnimVContext> contextFactory)
    : Repository<Schedule>(contextFactory), IScheduleRepository
{
    private readonly IDbContextFactory<StoronnimVContext> _contextFactory = contextFactory;

    public async Task<ScheduleFullProjection?> GetByIdAsNoTrackingAsync(long id, CancellationToken ct)
    {
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

        return result;
    }

    public async Task<IEnumerable<Schedule>?> GetAllSchedulesAsync(CancellationToken ct)
    {
        await using StoronnimVContext context = await _contextFactory.CreateDbContextAsync(ct);
        var dbSet = context.Schedules;
        var query = ApplyIncludes(dbSet);

        var result = await dbSet.ToListAsync(ct);

        return result;
    }

    public async Task<ScheduleShortProjection?> GetNearestScheduleForHomePageAsync(CancellationToken ct)
    {
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
                PerformanceDateTime = schedule.PerformanceDateTime,
                Location = schedule.Location
            })
            .FirstOrDefaultAsync(ct);

        return result;
    }

    public async Task<IEnumerable<ScheduleShortProjection>?> GetForPageAsync(int page, CancellationToken ct, int pageSize = 10, params object[] args)
    {
        await using StoronnimVContext context = await _contextFactory.CreateDbContextAsync(ct);
        var dbSet = context.Schedules;
        var query = ApplyIncludes(dbSet);

        var result = await query
            .AsNoTracking()
            .Where(schedule => schedule.Status == ScheduleStatus.Active)
            .OrderBy(schedule => schedule.PerformanceDateTime)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(schedule => new ScheduleShortProjection
            {
                Id = schedule.Id,
                Location = schedule.Location,
                PerformanceDateTime = schedule.PerformanceDateTime,
                Title = schedule.Title,
                Photo = schedule.Photo
            })
            .ToListAsync(ct);

        return result;
    }

    public async Task<int> GetTotalCountAsync(CancellationToken ct, params object[] args)
    {
        await using StoronnimVContext context = await _contextFactory.CreateDbContextAsync(ct);

        int count = await context.Schedules.CountAsync(ct);

        return count;
    }
}