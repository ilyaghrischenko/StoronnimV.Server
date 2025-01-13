using Microsoft.Extensions.Logging;
using StoronnimV.Application.Exceptions;
using StoronnimV.Application.Extensions;
using StoronnimV.Application.Interfaces.Entities;
using StoronnimV.Application.Models;
using StoronnimV.Domain.Interfaces;

namespace StoronnimV.Application.Services.Entities;

/// <summary>
/// Сервис для проверки полученных данных, полученых с репозитория. Так же используется сортировка
/// </summary>
/// <param name="newsRepository"></param>
public class NewsService(INewsRepository newsRepository,
    ILogger<NewsService> logger) : INewsService
{
    private readonly INewsRepository _newsRepository = newsRepository;
    private readonly ILogger<NewsService> _logger = logger;

    public async Task<object> GetItemByIdAsync(long id)
    {
        _logger.LogInformation($"Service: NewsService Method: GetItemByIdAsync with id: {id} started at {DateTime.UtcNow}");
        
        var newsItem = await _newsRepository.GetByIdAsNoTrackingAsync(id)
            ?? throw new EntityNotFoundException($"News with id: {id} was not found");
        
        _logger.LogInformation($"Service: NewsService Method: GetItemByIdAsync with id: {id} ended at {DateTime.UtcNow}");

        return newsItem;
    }
    
    public async Task<IEnumerable<object>> GetAllAsync()
    {
        _logger.LogInformation($"Service: NewsService Method: GetAllAsync started at {DateTime.UtcNow}");
        
        var allNews = await _newsRepository.GetAllAsync();
        if (allNews is null || !allNews.Any())
        {
            return new List<object>();
        }
        
        _logger.LogInformation($"Service: NewsService Method: GetAllAsync ended at {DateTime.UtcNow}");

        return allNews
            .OrderBy(news => (string)news.GetPropertyValue("Priority")!)
            .ThenByDescending(news => (string)news.GetPropertyValue("Date")!)
            .ToList();
    }

    public async Task<PaginationResult> GetForPageAsync(int page, int pageSize)
    {
        _logger.LogInformation($"Service: NewsService Method: GetForPageAsync with [page: {page}, pageSize: {pageSize}] started at {DateTime.UtcNow}");
        
        if (page <= 0)
        {
            throw new PaginationException("invalid page number");
        }
        
        var totalCount = await _newsRepository.GetTotalCountAsync();

        if (totalCount == 0)
        {
            return new PaginationResult(
                    currentPage: page,
                    totalPages: 0,
                    totalItems: 0,
                    items: Enumerable.Empty<object>()
                );
        }
        
        var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
        var items = await _newsRepository.GetForPageAsync(page, pageSize);

        if (items is null || !items.Any())
        {
            return new PaginationResult(
                currentPage: page,
                totalPages: 0,
                totalItems: 0,
                items: Enumerable.Empty<object>()
            );
        }
        
        var sortedItems = items.ToList();

        _logger.LogInformation($"Service: NewsService Method: GetForPageAsync with [page: {page}, pageSize: {pageSize}] ended at {DateTime.UtcNow}");

        return new PaginationResult(
            currentPage: page,
            totalPages: totalPages,
            totalItems: totalCount,
            items: sortedItems
        );
    }
}