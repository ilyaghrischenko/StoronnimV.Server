using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StoronnimV.Domain.Contracts;
using StoronnimV.Domain.Contracts.Database;
using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Enums;
using StoronnimV.Domain.Projections;
using StoronnimV.Domain.Projections.Admin;
using StoronnimV.Infrastructure.Repositories.Database.Shared;

namespace StoronnimV.Infrastructure.Repositories.Database;

public class AdminRepository(
    IDbContextFactory<StoronnimVContext> contextFactory,
    ILogger<AdminRepository> logger)
    : Repository<Admin>(contextFactory), IAdminRepository
{
    private readonly IDbContextFactory<StoronnimVContext> _contextFactory = contextFactory;
    private readonly ILogger<AdminRepository> _logger = logger;

    public async Task<AdminProjection?> GetByIdAsNoTrackingAsync(long id, CancellationToken ct)
    {
        await using StoronnimVContext context = await _contextFactory.CreateDbContextAsync(ct);
        var dbSet = context.Admins;
        var query = ApplyIncludes(dbSet);

        AdminProjection? result = await query
            .AsNoTracking()
            .Select(admin => new AdminProjection
            {
                Id = admin.Id,
                Login = admin.Login,
                Password = admin.Password
            })
            .FirstOrDefaultAsync(admin => admin.Id == id, ct);

        return result;
    }

    public async Task<Admin?> GetByLoginAsync(string login, CancellationToken ct)
    {
        await using StoronnimVContext context = await _contextFactory.CreateDbContextAsync(ct);
        var dbSet = context.Admins;
        var query = ApplyIncludes(dbSet);

        Admin? result = await query
            .AsNoTracking()
            .FirstOrDefaultAsync(admin => admin.Login == login, ct);

        return result;
    }

    public async Task<IEnumerable<BasicAdminProjection>?> GetAllBasicAdminsAsync(CancellationToken ct)
    {
        await using StoronnimVContext context = await _contextFactory.CreateDbContextAsync(ct);
        var dbSet = context.Admins;
        var query = ApplyIncludes(dbSet);

        var result = await query
            .AsNoTracking()
            .Where(admin => admin.Type == AdminType.Basic)
            .Select(admin => new BasicAdminProjection
            {
                Id = admin.Id,
                Login = admin.Login
            })
            .ToListAsync(ct);
        
        return result;
    }
}