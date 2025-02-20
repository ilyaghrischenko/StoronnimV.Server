using Microsoft.Extensions.Logging;
using StoronnimV.Application.Contracts.Entities;
using StoronnimV.Application.Exceptions;
using StoronnimV.Application.Models;
using StoronnimV.Domain.Contracts;
using StoronnimV.Domain.Contracts.Database;
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
        NewsFullProjection newsItem = await _newsRepository.GetByIdAsNoTrackingAsync(id, ct)
                                      ?? throw new EntityNotFoundException($"News with id: {id} was not found");

        return newsItem;
    }

    public async Task<PaginationResult<NewsPaginationProjection>> GetForPageAsync(int page, int pageSize, CancellationToken ct,
        params object[] args)
    {
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
                Items = []
            };
        }
    }
}