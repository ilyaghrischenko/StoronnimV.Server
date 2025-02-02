using Microsoft.Extensions.Logging;
using StoronnimV.Application.Exceptions;
using StoronnimV.Application.Extensions;
using StoronnimV.Application.Interfaces.Entities;
using StoronnimV.Application.Models;
using StoronnimV.Domain.Interfaces;
using StoronnimV.Domain.Projections.News;

namespace StoronnimV.Application.Services.Entities;

/// <summary>
/// Сервис для проверки полученных данных, полученых с репозитория. Так же используется сортировка
/// </summary>
/// <param name="newsRepository"></param>
public class NewsService(
    INewsRepository newsRepository,
    ILogger<NewsService> logger) : INewsService
{
    private readonly INewsRepository _newsRepository = newsRepository;
    private readonly ILogger<NewsService> _logger = logger;

    public async Task<NewsFullProjection> GetItemByIdAsync(long id, CancellationToken ct)
    {
        _logger.LogInformation(
            $"Service: NewsService Method: GetItemByIdAsync with id: {id} started at {DateTime.UtcNow}");

        NewsFullProjection newsItem = await _newsRepository.GetByIdAsNoTrackingAsync(id, ct)
                                      ?? throw new EntityNotFoundException($"News with id: {id} was not found");

        _logger.LogInformation(
            $"Service: NewsService Method: GetItemByIdAsync with id: {id} ended at {DateTime.UtcNow}");

        return newsItem;
    }

    // public async Task<IEnumerable<object>> GetAllAsync(CancellationToken ct)
    // {
    //     _logger.LogInformation($"Service: NewsService Method: GetAllAsync started at {DateTime.UtcNow}");
    //
    //     var allNews = await _newsRepository.GetAllAsync(ct);
    //     if (allNews is null || !allNews.Any())
    //     {
    //         return new List<object>();
    //     }
    //
    //     var result = allNews
    //         .OrderBy(news => (string)news.GetPropertyValue("Priority")!)
    //         .ThenByDescending(news => (string)news.GetPropertyValue("Date")!)
    //         .ToList();
    //
    //     _logger.LogInformation($"Service: NewsService Method: GetAllAsync ended at {DateTime.UtcNow}");
    //
    //     return result;
    // }

    public async Task<PaginationResult<NewsPaginationProjection>> GetForPageAsync(int page, int pageSize, CancellationToken ct,
        params object[] args)
    {
        _logger.LogInformation(
            $"Service: NewsService Method: GetForPageAsync with [page: {page}, pageSize: {pageSize}] started at {DateTime.UtcNow}");

        if (page <= 0)
        {
            throw new PaginationException("invalid page number");
        }

        int totalCount = await _newsRepository.GetTotalCountAsync(ct);

        try
        {
            if (totalCount == 0)
            {
                throw new PaginationException(string.Empty);
            }

            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var items = await _newsRepository.GetForPageAsync(page, ct, pageSize);

            if (items is null || !items.Any())
            {
                throw new PaginationException(string.Empty);
            }

            var sortedItems = items.ToList();

            PaginationResult<NewsPaginationProjection> response = new()
            {
                CurrentPage = page,
                TotalPages = totalPages,
                TotalItems = totalCount,
                Items = sortedItems
            };

            return response;
        }
        catch (PaginationException)
        {
            return new PaginationResult<NewsPaginationProjection>
            {
                CurrentPage = page,
                TotalPages = 0,
                TotalItems = 0,
                Items = Enumerable.Empty<NewsPaginationProjection>()
            };
        }
        finally
        {
            _logger.LogInformation($"Service: NewsService Method: GetForPageAsync with [page: {page}, pageSize: {pageSize}] ended at {DateTime.UtcNow}");
        }
    }

    public async Task<PaginationResult<NewsFullProjection>> GetForAdminPageAsync(int page, int pageSize, CancellationToken ct,
        params object[] args)
    {
        _logger.LogInformation(
            $"Service: NewsService Method: GetForPageAsync with [page: {page}, pageSize: {pageSize}] started at {DateTime.UtcNow}");

        if (page <= 0)
        {
            throw new PaginationException("invalid page number");
        }

        int totalCount = await _newsRepository.GetTotalCountAsync(ct);

        try
        {
            if (totalCount == 0)
            {
                throw new PaginationException(string.Empty);
            }

            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var items = await _newsRepository.GetForAdminPageAsync(page, ct, pageSize);

            if (items is null || !items.Any())
            {
                throw new PaginationException(string.Empty);
            }

            var sortedItems = items.ToList();

            PaginationResult<NewsFullProjection> response = new()
            {
                CurrentPage = page,
                TotalPages = totalPages,
                TotalItems = totalCount,
                Items = sortedItems
            };

            return response;
        }
        catch (PaginationException)
        {
            return new PaginationResult<NewsFullProjection>
            {
                CurrentPage = page,
                TotalPages = 0,
                TotalItems = 0,
                Items = Enumerable.Empty<NewsFullProjection>()
            };
        }
        finally
        {
            _logger.LogInformation($"Service: NewsService Method: GetForPageAsync with [page: {page}, pageSize: {pageSize}] ended at {DateTime.UtcNow}");
        }
    }
}