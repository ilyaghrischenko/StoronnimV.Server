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
public class NewsService(INewsRepository newsRepository) : INewsService
{
    private readonly INewsRepository _newsRepository = newsRepository;

    public async Task<object> GetItemByIdAsync(long id)
    {
        var newsItem = await _newsRepository.GetByIdAsNoTrackingAsync(id)
            ?? throw new EntityNotFoundException($"News with id: {id} was not found");
        
        return newsItem;
    }
    
    public async Task<IEnumerable<object>> GetAllAsync()
    {
        var allNews = await _newsRepository.GetAllAsync();
        if (allNews is null || !allNews.Any())
        {
            return new List<object>();
        }
        
        return allNews
            .OrderBy(news => (string)news.GetPropertyValue("Priority")!)
            .ThenByDescending(news => (string)news.GetPropertyValue("Date")!)
            .ToList();
    }

    public async Task<PaginationResult> GetForPageAsync(int page, int pageSize)
    {
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
        
        var totalPages = (int)Math.Ceiling((double)totalCount / page);
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
        
        var sortedItems = items
            .OrderBy(news => (string)news.GetPropertyValue("Priority")!)
            .ToList();

        return new PaginationResult(
            currentPage: page,
            totalPages: totalPages,
            totalItems: totalCount,
            items: sortedItems
        );

        // var allNews = await _newsRepository.GetForPageAsync(page);
        // if (allNews is null)
        // {
        //     return new List<object>();
        // }
        //
        // return allNews
        //     .OrderBy(news => (string)news.GetPropertyValue("Priority")!)
        //     .ToList();
    }
}