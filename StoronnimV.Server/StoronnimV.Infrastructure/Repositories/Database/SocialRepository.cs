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
public class SocialRepository(IDbContextFactory<StoronnimVContext> contextFactory,
    ILogger<SocialRepository> logger) : 
    Repository<Social>(contextFactory), ISocialRepository
{
    private readonly IDbContextFactory<StoronnimVContext> _contextFactory = contextFactory;
    private readonly ILogger<SocialRepository> _logger = logger;

    protected override IQueryable<Social> ApplyIncludes(IQueryable<Social> dbSet)
    {
        return dbSet.Include(social => social.Member);
    }

    public async Task<SocialProjection?> GetByIdAsNoTrackingAsync(long id, CancellationToken ct)
    {
        _logger.LogInformation($"Repository: SocialRepository Method: GetByIdAsNoTrackingAsync with id: {id} started at {DateTime.UtcNow}");
        
        await using StoronnimVContext context = await _contextFactory.CreateDbContextAsync(ct);
        var dbSet = context.Socials;
        var query = ApplyIncludes(dbSet);

        SocialProjection? result = await query
            .AsNoTracking()
            .Select(social => new SocialProjection
            {
                Id = social.Id,
                Type = social.Type,
                Url = social.Url
            })
            .FirstOrDefaultAsync(x => x.Id == id, ct);
        
        _logger.LogInformation($"Repository: SocialRepository Method: GetByIdAsNoTrackingAsync with id: {id} ended at {DateTime.UtcNow}");

        return result;
    }
}