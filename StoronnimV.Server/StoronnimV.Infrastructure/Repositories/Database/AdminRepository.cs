using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StoronnimV.Domain.Contracts;
using StoronnimV.Domain.Contracts.Database;
using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Projections;
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
        _logger.LogInformation(
            $"Repository: AdminRepository Method: GetByIdAsNoTrackingAsync with id: {id} started at {DateTime.UtcNow}");

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

        _logger.LogInformation(
            $"Repository: AdminRepository Method: GetByIdAsNoTrackingAsync with id: {id} ended at {DateTime.UtcNow}");

        return result;
    }

    public async Task<IEnumerable<AdminProjection>?> GetAllAsNoTrackingAsync(CancellationToken ct)
    {
        _logger.LogInformation($"Repository: AdminRepository Method: GetAllAsync started at {DateTime.UtcNow}");

        await using StoronnimVContext context = await _contextFactory.CreateDbContextAsync(ct);
        var dbSet = context.Admins;
        var query = ApplyIncludes(dbSet);

        var result = await query
            .AsNoTracking()
            .Select(admin => new AdminProjection
            {
                Id = admin.Id,
                Login = admin.Login,
                Password = admin.Password
            })
            .ToListAsync(ct);

        _logger.LogInformation($"Repository: AdminRepository Method: GetAllAsync ended at {DateTime.UtcNow}");

        return result;
    }

    public async Task<Admin?> GetByLoginAsync(string login, CancellationToken ct)
    {
        _logger.LogInformation($"Repository: AdminRepository Method: GetByLoginAsync started at {DateTime.UtcNow}");

        await using StoronnimVContext context = await _contextFactory.CreateDbContextAsync(ct);
        var dbSet = context.Admins;
        var query = ApplyIncludes(dbSet);

        Admin? result = await query
            .AsNoTracking()
            .FirstOrDefaultAsync(admin => admin.Login == login, ct);

        _logger.LogInformation($"Repository: AdminRepository Method: GetByLoginAsync ended at {DateTime.UtcNow}");

        return result;
    }
}