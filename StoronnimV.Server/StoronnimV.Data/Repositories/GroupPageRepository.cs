using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StoronnimV.Data.Repositories.Shared;
using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Interfaces;

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

    public async Task<object?> GetByIdAsNoTrackingAsync(long id)
    {
        _logger.LogInformation($"Repository: GroupPageRepository Method: GetByIdAsNoTrackingAsync with id: {id} started at {DateTime.UtcNow}");
        
        using var context = await _contextFactory.CreateDbContextAsync();
        var dbSet = context.GroupPages;
        var query = ApplyIncludes(dbSet);

        _logger.LogInformation($"Repository: GroupPageRepository Method: GetByIdAsNoTrackingAsync with id: {id} ended at {DateTime.UtcNow}");

        return await query
            .AsNoTracking()
            .Select(groupPage => new
            {
                Id = groupPage.Id,
                PhotoUrl = groupPage.PhotoUrl,
                Description = groupPage.Description
            })
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<object>?> GetAllAsync()
    {
        _logger.LogInformation($"Repository: GroupPageRepository Method: GetAllAsync started at {DateTime.UtcNow}");
        
        using var context = await _contextFactory.CreateDbContextAsync();
        var dbSet = context.GroupPages;
        var query = ApplyIncludes(dbSet);
        
        _logger.LogInformation($"Repository: GroupPageRepository Method: GetAllAsync ended at {DateTime.UtcNow}");

        return await query
            .AsNoTracking()
            .Select(groupPage => new
            {
                Id = groupPage.Id,
                PhotoUrl = groupPage.PhotoUrl,
                Description = groupPage.Description
            })
            .ToListAsync();
    }
    
    public async Task<object?> GetFirstGroupPageAsync()
    {
        _logger.LogInformation($"Repository: GroupPageRepository Method: GetFirstGroupPageAsync started at {DateTime.UtcNow}");
        
        using var context = await _contextFactory.CreateDbContextAsync();
        var dbSet = context.GroupPages;
        var query = ApplyIncludes(dbSet);
        
        _logger.LogInformation($"Repository: GroupPageRepository Method: GetFirstGroupPageAsync ended at {DateTime.UtcNow}");

        return await query
            .AsNoTracking()
            .Select(groupPage => new
            {
                Id = groupPage.Id,
                PhotoUrl = groupPage.PhotoUrl,
                Description = groupPage.Description
            })
            .FirstOrDefaultAsync();
    }
}