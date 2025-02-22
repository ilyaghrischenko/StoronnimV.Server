using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StoronnimV.Domain.Contracts;
using StoronnimV.Domain.Contracts.Database;
using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Projections;
using StoronnimV.Infrastructure.Repositories.Database.Shared;

namespace StoronnimV.Infrastructure.Repositories.Database;

/// <summary>
/// Репозиторий для получения данных напрямую с бд
/// </summary>
/// <param name="contextFactory"></param>
public class GroupPageRepository(IDbContextFactory<StoronnimVContext> contextFactory) : 
    Repository<GroupPage>(contextFactory), IGroupPageRepository
{
    private readonly IDbContextFactory<StoronnimVContext> _contextFactory = contextFactory;

    public async Task<GroupPageProjection?> GetByIdAsNoTrackingAsync(long id, CancellationToken ct)
    {
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
        
        return result;
    }

    public async Task<IEnumerable<GroupPageProjection>?> GetAllAsNoTrackingAsync(CancellationToken ct)
    {
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
        
        return result;
    }
    
    public async Task<GroupPageProjection?> GetFirstGroupPageAsync(CancellationToken ct)
    {
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
        
        return result;
    }
}