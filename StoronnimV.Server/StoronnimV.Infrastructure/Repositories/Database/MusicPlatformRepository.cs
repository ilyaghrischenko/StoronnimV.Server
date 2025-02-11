using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StoronnimV.Domain.Contracts;
using StoronnimV.Domain.Contracts.Database;
using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Projections;
using StoronnimV.Infrastructure.Repositories.Database.Shared;

namespace StoronnimV.Infrastructure.Repositories.Database;

public class MusicPlatformRepository(IDbContextFactory<StoronnimVContext> contextFactory,
    ILogger<MusicPlatformRepository> logger)
    : Repository<MusicPlatform>(contextFactory), IMusicPlatformRepository
{
    private readonly IDbContextFactory<StoronnimVContext> _contextFactory = contextFactory;
    private readonly ILogger<MusicPlatformRepository> _logger = logger;
    
    public async Task<MusicPlatformProjection?> GetByIdAsNoTrackingAsync(long id, CancellationToken ct)
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

    public async Task<IEnumerable<MusicPlatformProjection>?> GetAllAsNoTrackingAsync(CancellationToken ct)
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