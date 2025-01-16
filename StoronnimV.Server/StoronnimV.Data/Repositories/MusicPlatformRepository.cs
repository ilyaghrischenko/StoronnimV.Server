using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StoronnimV.Data.Repositories.Shared;
using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Interfaces;

namespace StoronnimV.Data.Repositories;

public class MusicPlatformRepository(IDbContextFactory<StoronnimVContext> contextFactory,
    ILogger<MusicPlatformRepository> logger)
    : Repository<MusicPlatform>(contextFactory), IMusicPlatformRepository
{
    private readonly IDbContextFactory<StoronnimVContext> _contextFactory = contextFactory;
    private readonly ILogger<MusicPlatformRepository> _logger = logger;
    
    public async Task<object?> GetByIdAsNoTrackingAsync(long id)
    {
        _logger.LogInformation($"Repository: MusicPlatformRepository Method: GetByIdAsNoTrackingAsync with id: {id} started at {DateTime.UtcNow}");
        
        using var context = await _contextFactory.CreateDbContextAsync();
        var dbSet = context.MusicPlatforms;
        var query = ApplyIncludes(dbSet);
        
        _logger.LogInformation($"Repository: MusicPlatformRepository Method: GetByIdAsNoTrackingAsync with id: {id} ended at {DateTime.UtcNow}");
        
        return await query
            .AsNoTracking()
            .Select(musicPlatform => new
            {
                Id = musicPlatform.Id,
                BgImageUrl = musicPlatform.BgImageUrl,
                PlatformUrl = musicPlatform.PlatformUrl
            })
            .FirstOrDefaultAsync(musicPlatform => musicPlatform.Id == id);
    }

    public async Task<IEnumerable<object>?> GetAllAsync()
    {
        _logger.LogInformation($"Repository: MusicPlatformRepository Method: GetAllAsync started at {DateTime.UtcNow}");
        
        using var context = await _contextFactory.CreateDbContextAsync();
        var dbSet = context.MusicPlatforms;
        var query = ApplyIncludes(dbSet);
        
        _logger.LogInformation($"Repository: MusicPlatformRepository Method: GetAllAsync ended at {DateTime.UtcNow}");

        return await query
            .AsNoTracking()
            .Select(musicPlatform => new
            {
                Id = musicPlatform.Id,
                BgImageUrl = musicPlatform.BgImageUrl,
                PlatformUrl = musicPlatform.PlatformUrl
            })
            .ToListAsync();
    }
}