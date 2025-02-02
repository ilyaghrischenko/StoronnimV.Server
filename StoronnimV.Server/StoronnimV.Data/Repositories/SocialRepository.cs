using System.Xml;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StoronnimV.Data.Repositories.Shared;
using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Interfaces;
using StoronnimV.Domain.Projections.Social;

namespace StoronnimV.Data.Repositories;

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

    public async Task<SocialShortProjection?> GetByIdAsNoTrackingAsync(long id, CancellationToken ct)
    {
        _logger.LogInformation($"Repository: SocialRepository Method: GetByIdAsNoTrackingAsync with id: {id} started at {DateTime.UtcNow}");
        
        await using StoronnimVContext context = await _contextFactory.CreateDbContextAsync(ct);
        var dbSet = context.Socials;
        var query = ApplyIncludes(dbSet);

        SocialShortProjection? result = await query
            .AsNoTracking()
            .Select(social => new SocialShortProjection
            {
                Id = social.Id,
                Type = social.Type,
                Url = social.Url
            })
            .FirstOrDefaultAsync(x => x.Id == id, ct);
        
        _logger.LogInformation($"Repository: SocialRepository Method: GetByIdAsNoTrackingAsync with id: {id} ended at {DateTime.UtcNow}");

        return result;
    }
    
    public async Task<IEnumerable<SocialShortProjection>?> GetAllForMemberAsync(long memberId, CancellationToken ct)
    {
        _logger.LogInformation($"Repository: SocialRepository Method: GetAllForMemberAsync with memberId: {memberId} started at {DateTime.UtcNow}");
        
        await using StoronnimVContext context = await _contextFactory.CreateDbContextAsync(ct);
        var dbSet = context.Socials;
        var query = ApplyIncludes(dbSet);
        
        var result = await query
            .AsNoTracking()
            .Where(social => social.Member.Id == memberId)
            .Select(social => new SocialShortProjection
            {
                Id = social.Id,
                Type = social.Type,
                Url = social.Url
            })
            .ToListAsync(ct);
        
        _logger.LogInformation($"Repository: SocialRepository Method: GetAllForMemberAsync with memberId: {memberId} ended at {DateTime.UtcNow}");

        return result;
    }

}