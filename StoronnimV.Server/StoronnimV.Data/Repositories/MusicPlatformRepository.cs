using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StoronnimV.Data.Repositories.Shared;
using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Interfaces;
using StoronnimV.Domain.Projections;

namespace StoronnimV.Data.Repositories;

public class MusicPlatformRepository(IDbContextFactory<StoronnimVContext> contextFactory,
    ILogger<MusicPlatformRepository> logger)
    : Repository<MusicPlatform>(contextFactory), IMusicPlatformRepository
{
    private readonly IDbContextFactory<StoronnimVContext> _contextFactory = contextFactory;
    private readonly ILogger<MusicPlatformRepository> _logger = logger;
    
    public async Task<object?> GetByIdAsNoTrackingAsync(long id, CancellationToken ct)
    {
        _logger.LogInformation($"Repository: MusicPlatformRepository Method: GetByIdAsNoTrackingAsync with id: {id} started at {DateTime.UtcNow}");
        
        await using StoronnimVContext context = await _contextFactory.CreateDbContextAsync(ct);
        var dbSet = context.MusicPlatforms;
        var query = ApplyIncludes(dbSet);
        
        MusicPlatformProjection? result = await query
            .AsNoTracking()
            .Select(musicPlatform => new MusicPlatformProjection
            {
                Id = musicPlatform.Id,
                BgImageUrl = musicPlatform.BgImageUrl,
                PlatformUrl = musicPlatform.PlatformUrl
            })
            .FirstOrDefaultAsync(musicPlatform => musicPlatform.Id == id, ct);
        
        _logger.LogInformation($"Repository: MusicPlatformRepository Method: GetByIdAsNoTrackingAsync with id: {id} ended at {DateTime.UtcNow}");

        return result;
    }

    public async Task<IEnumerable<object>?> GetAllAsync(CancellationToken ct)
    {
        _logger.LogInformation($"Repository: MusicPlatformRepository Method: GetAllAsync started at {DateTime.UtcNow}");
        
        await using StoronnimVContext context = await _contextFactory.CreateDbContextAsync(ct);
        var dbSet = context.MusicPlatforms;
        var query = ApplyIncludes(dbSet);
        
        var result = await query
            .AsNoTracking()
            .Select(musicPlatform => new MusicPlatformProjection
            {
                Id = musicPlatform.Id,
                BgImageUrl = musicPlatform.BgImageUrl,
                PlatformUrl = musicPlatform.PlatformUrl
            })
            .ToListAsync(ct);
        
        _logger.LogInformation($"Repository: MusicPlatformRepository Method: GetAllAsync ended at {DateTime.UtcNow}");

        return result;
    }
}