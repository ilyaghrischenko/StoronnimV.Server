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

    public async Task<object?> GetByIdAsNoTrackingAsync(long id, CancellationToken ct)
    {
        _logger.LogInformation($"Repository: MemberRepository Method: GetByIdAsNoTrackingAsync with id: {id} started at {DateTime.UtcNow}");
        
        using var context = await _contextFactory.CreateDbContextAsync(ct);
        var dbSet = context.Members;
        var query = ApplyIncludes(dbSet);

        var result = await query
            .AsNoTracking()
            .Select(member => new
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

    public async Task<IEnumerable<object>?> GetAllAsync(CancellationToken ct)
    {
        _logger.LogInformation($"Repository: MemberRepository Method: GetAllAsync started at {DateTime.UtcNow}");
        
        using var context = await _contextFactory.CreateDbContextAsync(ct);
        var dbSet = context.Members;
        var query = ApplyIncludes(dbSet);
        
        var result = await query
            .AsNoTracking()
            .Select(member => new
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
}