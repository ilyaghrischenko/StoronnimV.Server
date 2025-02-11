using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StoronnimV.Domain.Contracts;
using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Projections;
using StoronnimV.Domain.Projections.Member;
using StoronnimV.Infrastructure.Repositories.Shared;

namespace StoronnimV.Infrastructure.Repositories;

/// <summary>
/// Репозиторий для получения данных напрямую с бд
/// </summary>
/// <param name="contextFactory"></param>
public class MemberRepository(IDbContextFactory<StoronnimVContext> contextFactory,
    ILogger<MemberRepository> logger) : 
    Repository<Member>(contextFactory), IMemberRepository
{
    private readonly IDbContextFactory<StoronnimVContext> _contextFactory = contextFactory;
    private readonly ILogger<MemberRepository> _logger = logger;

    protected override IQueryable<Member> ApplyIncludes(IQueryable<Member> dbSet)
    {
        return dbSet.Include(member => member.Socials);
    }

    public async Task<MemberFullProjection?> GetByIdAsNoTrackingAsync(long id, CancellationToken ct)
    {
        _logger.LogInformation($"Repository: MemberRepository Method: GetByIdAsNoTrackingAsync with id: {id} started at {DateTime.UtcNow}");
        
        await using StoronnimVContext context = await _contextFactory.CreateDbContextAsync(ct);
        var dbSet = context.Members;
        var query = ApplyIncludes(dbSet);

        MemberFullProjection? result = await query
            .AsNoTracking()
            .Select(member => new MemberFullProjection
            {
                Id = member.Id,
                PhotoUrl = member.PhotoUrl,
                FullName = member.FullName,
                Description = member.Description,
                Role = member.Role
            })
            .FirstOrDefaultAsync(x => x.Id == id, ct);
        
        _logger.LogInformation($"Repository: MemberRepository Method: GetByIdAsNoTrackingAsync with id: {id} ended at {DateTime.UtcNow}");

        return result;
    }

    public async Task<IEnumerable<MemberShortProjection>?> GetAllAsNoTrackingAsync(CancellationToken ct)
    {
        _logger.LogInformation($"Repository: MemberRepository Method: GetAllAsync started at {DateTime.UtcNow}");
        
        await using StoronnimVContext context = await _contextFactory.CreateDbContextAsync(ct);
        var dbSet = context.Members;
        var query = ApplyIncludes(dbSet);
        
        var result = await query
            .AsNoTracking()
            .Select(member => new MemberShortProjection
            {
                Id = member.Id,
                PhotoUrl = member.PhotoUrl,
                FullName = member.FullName,
                Role = member.Role
            })
            .ToListAsync(ct);
        
        _logger.LogInformation($"Repository: MemberRepository Method: GetAllAsync ended at {DateTime.UtcNow}");

        return result;
    }

    public async Task<IEnumerable<MemberFullProjection>?> GetAllForAdminAsync(CancellationToken ct)
    {
        _logger.LogInformation($"Repository: MemberRepository Method: GetAllForAdminAsync started at {DateTime.UtcNow}");
        
        await using StoronnimVContext context = await _contextFactory.CreateDbContextAsync(ct);
        var dbSet = context.Members;
        var query = ApplyIncludes(dbSet);

        var result = await query
            .AsNoTracking()
            .Select(item => new MemberFullProjection
            {
                Id = item.Id,
                Description = item.Description,
                FullName = item.FullName,
                PhotoUrl = item.PhotoUrl,
                Role = item.Role
            })
            .ToListAsync(ct);

        _logger.LogInformation($"Repository: MemberRepository Method: GetAllForAdminAsync ended at {DateTime.UtcNow}");

        return result;
    }
}