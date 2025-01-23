using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StoronnimV.Data.Repositories.Shared;
using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Interfaces;

namespace StoronnimV.Data.Repositories;

public class AdminRepository(IDbContextFactory<StoronnimVContext> contextFactory,
    ILogger<AdminRepository> logger)
    : Repository<Admin>(contextFactory), IAdminRepository
{
    private readonly IDbContextFactory<StoronnimVContext> _contextFactory = contextFactory;
    private readonly ILogger<AdminRepository> _logger = logger;
    
    public async Task<object?> GetByIdAsNoTrackingAsync(long id)
    {
        _logger.LogInformation($"Repository: AdminRepository Method: GetByIdAsNoTrackingAsync with id: {id} started at {DateTime.UtcNow}");
        
        using var context = await _contextFactory.CreateDbContextAsync();
        var dbSet = context.Admins;
        var query = ApplyIncludes(dbSet);
        
        var result = await query
            .AsNoTracking()
            .Select(admin => new
            {
                Id = admin.Id,
                Login = admin.Login,
                Password = admin.Password
            })
            .FirstOrDefaultAsync(admin => admin.Id == id);
        
        _logger.LogInformation($"Repository: AdminRepository Method: GetByIdAsNoTrackingAsync with id: {id} ended at {DateTime.UtcNow}");

        return result;
    }

    public async Task<IEnumerable<object>?> GetAllAsync()
    {
        _logger.LogInformation($"Repository: AdminRepository Method: GetAllAsync started at {DateTime.UtcNow}");
        
        using var context = await _contextFactory.CreateDbContextAsync();
        var dbSet = context.Admins;
        var query = ApplyIncludes(dbSet);
        
        var result = await query
            .AsNoTracking()
            .Select(admin => new
            {
                Id = admin.Id,
                Login = admin.Login,
                Password = admin.Password
            })
            .ToListAsync();
        
        _logger.LogInformation($"Repository: AdminRepository Method: GetAllAsync ended at {DateTime.UtcNow}");

        return result;
    }

    public async Task<Admin?> GetByLoginAsync(string login)
    {
        _logger.LogInformation($"Repository: AdminRepository Method: GetByLoginAsync started at {DateTime.UtcNow}");
        
        using var context = await _contextFactory.CreateDbContextAsync();
        var dbSet = context.Admins;
        var query = ApplyIncludes(dbSet);
        
        var result = await query
            .AsNoTracking()
            .FirstOrDefaultAsync(admin => admin.Login == login);
        
        _logger.LogInformation($"Repository: AdminRepository Method: GetByLoginAsync ended at {DateTime.UtcNow}");

        return result;
    }
}