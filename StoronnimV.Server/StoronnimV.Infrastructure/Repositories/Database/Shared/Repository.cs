using Microsoft.EntityFrameworkCore;
using StoronnimV.Domain.Contracts.Database.Shared;
using StoronnimV.Domain.Entities.Shared;

namespace StoronnimV.Infrastructure.Repositories.Database.Shared;

/// <summary>
/// Общий репозиторий (Generic), нужен для круд запросов для каждой из сущностей
/// </summary>
/// <param name="contextFactory"></param>
/// <typeparam name="T">Entity</typeparam>
public class Repository<T>(IDbContextFactory<StoronnimVContext> contextFactory)
    : IRepository<T> where T : BaseEntity
{
    protected virtual IQueryable<T> ApplyIncludes(IQueryable<T> dbSet)
    {
        return dbSet;
    }
    
    public async Task<T?> GetByIdAsync(long id, CancellationToken ct)
    {
        await using StoronnimVContext context = await contextFactory.CreateDbContextAsync(ct);
        var dbSet = context.Set<T>();
        var query = ApplyIncludes(dbSet);

        return await query
            .FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task AddAsync(T entity, CancellationToken ct)
    {
        await using StoronnimVContext context = await contextFactory.CreateDbContextAsync(ct);
        var dbSet = context.Set<T>();
        
        await dbSet.AddAsync(entity, ct);
        await context.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(T entity, Action updateAction, CancellationToken ct)
    {
        await using StoronnimVContext context = await contextFactory.CreateDbContextAsync(ct);
        var dbSet = context.Set<T>();

        dbSet.Update(entity);
        updateAction();
        
        await context.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(T entity, CancellationToken ct)
    {
        await using StoronnimVContext context = await contextFactory.CreateDbContextAsync(ct);
        var dbSet = context.Set<T>();
        
        dbSet.Remove(entity);
        await context.SaveChangesAsync(ct);
    }
}