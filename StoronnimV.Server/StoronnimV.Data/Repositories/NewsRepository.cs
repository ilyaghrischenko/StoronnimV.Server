using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StoronnimV.Data.Repositories.Shared;
using StoronnimV.Domain.Contracts;
using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Enums;
using StoronnimV.Domain.Projections.News;

namespace StoronnimV.Data.Repositories;

/// <summary>
/// Репозиторий для конкретной сущности, нужен для описания метода с инклудами, а так же для специальных селект методов
/// </summary>
/// <param name="contextFactory"></param>
public class NewsRepository(
    IDbContextFactory<StoronnimVContext> contextFactory,
    ILogger<NewsRepository> logger
    ) : Repository<News>(contextFactory), INewsRepository
{
    private readonly IDbContextFactory<StoronnimVContext> _contextFactory = contextFactory;
    private readonly ILogger<NewsRepository> _logger = logger;

    protected override IQueryable<News> ApplyIncludes(IQueryable<News> dbSet)
    {
        return dbSet.Include(news => news.Video);
    }

    public async Task<NewsFullProjection?> GetByIdAsNoTrackingAsync(long id, CancellationToken ct)
    {
        _logger.LogInformation($"Repository: NewsRepository Method: GetByIdAsNoTrackingAsync with id: {id} started at {DateTime.UtcNow}");
        
        await using StoronnimVContext context = await _contextFactory.CreateDbContextAsync(ct);
        var dbSet = context.NewsItems;
        var query = ApplyIncludes(dbSet);

        NewsFullProjection? result = await query
            .AsNoTracking()
            .Select(newsItem => new NewsFullProjection
            {
                Id = newsItem.Id,
                Photo = newsItem.Photo,
                Video = newsItem.Video.Url,
                Title = newsItem.Title,
                Description = newsItem.Description,
                Priority = newsItem.Priority,
                Date = newsItem.Date
            })
            .FirstOrDefaultAsync(x => x.Id == id, ct);
        
        _logger.LogInformation($"Repository: NewsRepository Method: GetByIdAsNoTrackingAsync with id: {id} ended at {DateTime.UtcNow}");

        return result;
    }

    public async Task<IEnumerable<NewsPaginationProjection>?> GetForPageAsync(int page, CancellationToken ct, int pageSize, params object[] args)
    {
        _logger.LogInformation($"Repository: NewsRepository Method: GetForPageAsync with [page: {page}, pageSize: {pageSize}] started at {DateTime.UtcNow}");
        
        await using StoronnimVContext context = await _contextFactory.CreateDbContextAsync(ct);
        var dbSet = context.NewsItems;
        var query = ApplyIncludes(dbSet);
        
        var result = await query
            .AsNoTracking()
            .OrderByDescending(newsItem => newsItem.Date)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(newsItem => new NewsPaginationProjection
            {
                Id = newsItem.Id,
                Photo = newsItem.Photo,
                Title = newsItem.Title,
                Priority = newsItem.Priority,
                Date = newsItem.Date
            })
            .ToListAsync(ct);
        
        _logger.LogInformation($"Repository: NewsRepository Method: GetForPageAsync with [page: {page}, pageSize: {pageSize}] ended at {DateTime.UtcNow}");

        return result;
    }

    public async Task<IEnumerable<NewsFullProjection>?> GetForAdminPageAsync(int page, CancellationToken ct, int pageSize = 10, params object[] args)
    {
        _logger.LogInformation($"Repository: NewsRepository Method: GetForAdminPageAsync with [page: {page}, pageSize: {pageSize}] started at {DateTime.UtcNow}");
        
        await using StoronnimVContext context = await _contextFactory.CreateDbContextAsync(ct);
        var dbSet = context.NewsItems;
        var query = ApplyIncludes(dbSet);
        
        var result = await query
            .AsNoTracking()
            .OrderByDescending(newsItem => newsItem.Date)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(newsItem => new NewsFullProjection
            {
                Id = newsItem.Id,
                Photo = newsItem.Photo,
                Video = newsItem.Video.Url,
                Title = newsItem.Title,
                Description = newsItem.Description,
                Priority = newsItem.Priority,
                Date = newsItem.Date
            })
            .ToListAsync(ct);
        
        _logger.LogInformation($"Repository: NewsRepository Method: GetForAdminPageAsync with [page: {page}, pageSize: {pageSize}] ended at {DateTime.UtcNow}");

        return result;
    }

    public async Task<int> GetTotalCountAsync(CancellationToken ct, params object[] args)
    {
        _logger.LogInformation($"Repository: NewsRepository Method: GetTotalCountAsync started at {DateTime.UtcNow}");
        
        await using StoronnimVContext context = await _contextFactory.CreateDbContextAsync(ct);
        
        int result = await context.NewsItems.CountAsync(ct);
        
        _logger.LogInformation($"Repository: NewsRepository Method: GetTotalCountAsync ended at {DateTime.UtcNow}");

        return result;
    }
    
    public async Task<IEnumerable<NewsHomeProjection>?> GetMainNewsForHomePageAsync(int count, CancellationToken ct)
    {
        _logger.LogInformation($"Repository: NewsRepository Method: GetNewsForHomePageAsync with count: {count} started at {DateTime.UtcNow}");
        
        await using StoronnimVContext context = await _contextFactory.CreateDbContextAsync(ct);
        var dbSet = context.NewsItems;
        var query = ApplyIncludes(dbSet);
        
        var result = await query
            .AsNoTracking()
            .Where(newsItem => newsItem.Priority == NewsPriority.Main)
            .OrderByDescending(newsItem => newsItem.Date)
            .Take(count)
            .Select(newsItem => new NewsHomeProjection
            {
                Id = newsItem.Id,
                Photo = newsItem.Photo,
                Title = newsItem.Title
            })
            .ToListAsync(ct);
        
        _logger.LogInformation($"Repository: NewsRepository Method: GetNewsForHomePageAsync with count: {count} ended at {DateTime.UtcNow}");

        return result;
    }

    public async Task<IEnumerable<NewsFullProjection>?> GetItemsByTitle(string title, CancellationToken ct)
    {
        _logger.LogInformation($"Repository: NewsRepository Method: GetItemsByTitle with title: {title} started at {DateTime.UtcNow}");
        
        await using StoronnimVContext context = await _contextFactory.CreateDbContextAsync(ct);
        var dbSet = context.NewsItems;
        var query = ApplyIncludes(dbSet);
        
        var itemsByTitle = await query
            .AsNoTracking()
            .Select(newsItem => new NewsFullProjection
            {
                Id = newsItem.Id,
                Date = newsItem.Date,
                Description = newsItem.Description,
                Photo = newsItem.Photo,
                Priority = newsItem.Priority,
                Title = newsItem.Title,
                Video = newsItem.Video.Url
            })
            .Where(newsItem => newsItem.Title.Trim().ToLower().Contains(title))
            .OrderByDescending(newsItem => newsItem.Date)
            .ToListAsync(ct);
        
        _logger.LogInformation($"Repository: NewsRepository Method: GetItemsByTitle with title: {title} ended at {DateTime.UtcNow}");

        return itemsByTitle;
    }
}