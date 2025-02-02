using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StoronnimV.Data.Repositories.Shared;
using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Interfaces;
using StoronnimV.Domain.Projections;

namespace StoronnimV.Data.Repositories;

/// <summary>
/// Репозиторий для получения данных напрямую с бд
/// </summary>
/// <param name="contextFactory"></param>
public class GroupPageRepository(IDbContextFactory<StoronnimVContext> contextFactory,
    ILogger<GroupPageRepository> logger) : 
    Repository<GroupPage>(contextFactory), IGroupPageRepository
{
    private readonly IDbContextFactory<StoronnimVContext> _contextFactory = contextFactory;
    private readonly ILogger<GroupPageRepository> _logger = logger;

    public async Task<object?> GetByIdAsNoTrackingAsync(long id, CancellationToken ct)
    {
        _logger.LogInformation($"Repository: GroupPageRepository Method: GetByIdAsNoTrackingAsync with id: {id} started at {DateTime.UtcNow}");
        
        await using StoronnimVContext context = await _contextFactory.CreateDbContextAsync(ct);
        var dbSet = context.GroupPages;
        var query = ApplyIncludes(dbSet);

        GroupPageProjection? result = await query
            .AsNoTracking()
            .Select(groupPage => new GroupPageProjection
            {
                Id = groupPage.Id,
                PhotoUrl = groupPage.PhotoUrl,
                Description = groupPage.Description
            })
            .FirstOrDefaultAsync(x => x.Id == id, ct);
        
        _logger.LogInformation($"Repository: GroupPageRepository Method: GetByIdAsNoTrackingAsync with id: {id} ended at {DateTime.UtcNow}");

        return result;
    }

    public async Task<IEnumerable<object>?> GetAllAsync(CancellationToken ct)
    {
        _logger.LogInformation($"Repository: GroupPageRepository Method: GetAllAsync started at {DateTime.UtcNow}");
        
        await using StoronnimVContext context = await _contextFactory.CreateDbContextAsync(ct);
        var dbSet = context.GroupPages;
        var query = ApplyIncludes(dbSet);
        
        var result = await query
            .AsNoTracking()
            .Select(groupPage => new GroupPageProjection
            {
                Id = groupPage.Id,
                PhotoUrl = groupPage.PhotoUrl,
                Description = groupPage.Description
            })
            .ToListAsync(ct);
        
        _logger.LogInformation($"Repository: GroupPageRepository Method: GetAllAsync ended at {DateTime.UtcNow}");

        return result;
    }
    
    public async Task<object?> GetFirstGroupPageAsync(CancellationToken ct)
    {
        _logger.LogInformation($"Repository: GroupPageRepository Method: GetFirstGroupPageAsync started at {DateTime.UtcNow}");
        
        await using StoronnimVContext context = await _contextFactory.CreateDbContextAsync(ct);
        var dbSet = context.GroupPages;
        var query = ApplyIncludes(dbSet);
        
        GroupPageProjection? result = await query
            .AsNoTracking()
            .Select(groupPage => new GroupPageProjection
            {
                Id = groupPage.Id,
                PhotoUrl = groupPage.PhotoUrl,
                Description = groupPage.Description
            })
            .FirstOrDefaultAsync(ct);
        
        _logger.LogInformation($"Repository: GroupPageRepository Method: GetFirstGroupPageAsync ended at {DateTime.UtcNow}");

        return result;
    }
}