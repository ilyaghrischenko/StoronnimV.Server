using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StoronnimV.Domain.Contracts;
using StoronnimV.Domain.Contracts.Database;
using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Projections;
using StoronnimV.Domain.Projections.Member;
using StoronnimV.Infrastructure.Repositories.Database.Shared;

namespace StoronnimV.Infrastructure.Repositories.Database;

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
                Role = member.Role,
                Socials = member.Socials.Select(social => new SocialProjection
                {
                    Id = social.Id,
                    Type = social.Type,
                    Url = social.Url
                })
            })
            .FirstOrDefaultAsync(x => x.Id == id, ct);
        
        return result;
    }

    public async Task<IEnumerable<MemberShortProjection>?> GetAllAsNoTrackingAsync(CancellationToken ct)
    {
        await using StoronnimVContext context = await _contextFactory.CreateDbContextAsync(ct);
        
        var result = await context.Members
            .AsNoTracking()
            .Select(member => new MemberShortProjection
            {
                Id = member.Id,
                PhotoUrl = member.PhotoUrl,
                FullName = member.FullName,
                Role = member.Role
            })
            .ToListAsync(ct);
        
        return result;
    }
}