using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StoronnimV.Data.Repositories.Shared;
using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Interfaces;

namespace StoronnimV.Data.Repositories;

/// <summary>
/// Репозиторий для конкретной сущности, нужен для описания метода с инклудами, а так же для специальных селект методов
/// </summary>
/// <param name="contextFactory"></param>
public class NewsRepository(IDbContextFactory<StoronnimVContext> contextFactory,
    ILogger<NewsRepository> logger)
    : Repository<News>(contextFactory), INewsRepository
{
    private readonly IDbContextFactory<StoronnimVContext> _contextFactory = contextFactory;
    private readonly ILogger<NewsRepository> _logger = logger;

    protected override IQueryable<News> ApplyIncludes(IQueryable<News> dbSet)
    {
        return dbSet.Include(news => news.Video);
    }

    public async Task<object?> GetByIdAsNoTrackingAsync(long id)
    {
        _logger.LogInformation($"Repository: NewsRepository Method: GetByIdAsNoTrackingAsync with id: {id} started at {DateTime.UtcNow}");
        
        using var context = await _contextFactory.CreateDbContextAsync();
        var dbSet = context.NewsItems;
        var query = ApplyIncludes(dbSet);

        _logger.LogInformation($"Repository: NewsRepository Method: GetByIdAsNoTrackingAsync with id: {id} ended at {DateTime.UtcNow}");

        return await query
            .AsNoTracking()
            .Select(newsItem => new
            {
                Id = newsItem.Id,
                Photo = newsItem.Photo,
                Video = newsItem.Video.Url,
                Title = newsItem.Title,
                Description = newsItem.Description,
                Priority = newsItem.Priority.ToString(),
                Date = newsItem.Date.ToShortDateString()
            })
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<object>?> GetAllAsync()
    {
        _logger.LogInformation($"Repository: NewsRepository Method: GetAllAsync started at {DateTime.UtcNow}");
        
        using var context = await _contextFactory.CreateDbContextAsync();
        var dbSet = context.NewsItems;
        var query = ApplyIncludes(dbSet);
        
        _logger.LogInformation($"Repository: NewsRepository Method: GetAllAsync ended at {DateTime.UtcNow}");

        return await query
            .AsNoTracking()
            .Select(newsItem => new
            {
                Id = newsItem.Id,
                Photo = newsItem.Photo,
                Title = newsItem.Title,
                Description = newsItem.Description,
                Priority = newsItem.Priority.ToString(),
                Date = newsItem.Date.ToShortDateString()
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<object>?> GetForPageAsync(int page, int pageSize = 9)
    {
        _logger.LogInformation($"Repository: NewsRepository Method: GetForPageAsync with [page: {page}, pageSize: {pageSize}] started at {DateTime.UtcNow}");
        
        using var context = await _contextFactory.CreateDbContextAsync();
        var dbSet = context.NewsItems;
        var query = ApplyIncludes(dbSet);
        
        _logger.LogInformation($"Repository: NewsRepository Method: GetForPageAsync with [page: {page}, pageSize: {pageSize}] ended at {DateTime.UtcNow}");

        return await query
            .AsNoTracking()
            .OrderByDescending(newsItem => newsItem.Date)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(newsItem => new
            {
                Id = newsItem.Id,
                Photo = newsItem.Photo,
                Title = newsItem.Title,
                Priority = newsItem.Priority.ToString(),
                Date = newsItem.Date.ToShortDateString()
            })
            .ToListAsync();
    }

    public async Task<int> GetTotalCountAsync()
    {
        _logger.LogInformation($"Repository: NewsRepository Method: GetTotalCountAsync started at {DateTime.UtcNow}");
        
        using var context = await _contextFactory.CreateDbContextAsync();
        
        _logger.LogInformation($"Repository: NewsRepository Method: GetTotalCountAsync ended at {DateTime.UtcNow}");

        return await context.NewsItems.CountAsync();
    }
}