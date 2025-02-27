using System.Globalization;
using Microsoft.Extensions.Logging;
using StoronnimV.Application.Contracts.Entities;
using StoronnimV.Application.DTO.Requests.Entities.Pages.Addition;
using StoronnimV.Application.Exceptions;
using StoronnimV.Application.Models;
using StoronnimV.Domain.Contracts;
using StoronnimV.Domain.Contracts.AzureBlobStorage;
using StoronnimV.Domain.Contracts.Database;
using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Enums;
using StoronnimV.Domain.Projections.News;

namespace StoronnimV.Application.Services.Entities;

/// <summary>
/// Сервис для проверки полученных данных, полученых с репозитория. Так же используется сортировка
/// </summary>
/// <param name="newsRepository"></param>
public class NewsService(
    INewsRepository newsRepository,
    IVideoRepository videoRepository,
    IBlobRepository blobRepository) : INewsService
{
    public async Task<NewsFullProjection> GetItemByIdAsync(long id, CancellationToken ct)
    {
        NewsFullProjection newsItem = await newsRepository.GetByIdAsNoTrackingAsync(id, ct)
                                      ?? throw new EntityNotFoundException($"News with {nameof(id)}: {id} was not found");

        return newsItem;
    }

    public async Task<PaginationResult<NewsPaginationProjection>> GetForPageAsync(int page, int pageSize, CancellationToken ct,
        params object[] args)
    {
        if (page <= 0)
        {
            throw new PaginationException("Invalid page number");
        }

        int totalCount = await newsRepository.GetTotalCountAsync(ct);

        try
        {
            if (totalCount == 0)
            {
                throw new PaginationException(string.Empty);
            }

            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var items = await newsRepository.GetForPageAsync(page, ct, pageSize);

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

    /// <summary>
    /// News item addition to the database
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    public async Task AddNewsItemAsync(NewsItemAdditionRequest request, CancellationToken ct)
    {
        Video? newsVideo = null;
        if (request.VideoId != null)
        {
            newsVideo = await videoRepository.GetByIdAsync(request.VideoId.Value, ct);
        }

        News newsItem = new News
        {
            Title = request.Title,
            Description = request.Description,
            Video = newsVideo,
            Priority = Enum.Parse<NewsPriority>(request.Priority),
            Date = DateOnly.TryParseExact(request.Date, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateOnly date)
                ? date
                : DateOnly.FromDateTime(DateTime.UtcNow),
        };

        await newsRepository.AddAsync(newsItem, ct);

        if (request.Photo != null)
        {
            string photoUrl = await blobRepository.AddFileAndGetUrlAsync("storonnimv-photo", $"news-{newsItem.Id}",
                request.Photo.OpenReadStream(), ct);
            await newsRepository.UpdateAsync(newsItem, () => newsItem.Photo = photoUrl, ct);
        }          
    }
    
    /// <summary>
    /// News item deletion from the database
    /// </summary>
    /// <param name="id"></param>
    /// <param name="ct"></param>
    /// <exception cref="EntityNotFoundException"></exception>
    public async Task DeleteNewsItemAsync(long id, CancellationToken ct)
    {
        News? newsItem = await newsRepository.GetByIdAsync(id, ct);

        if (newsItem is null)
        {
            throw new EntityNotFoundException($"NewsItem with {nameof(id)}: {id} was not found");
        }

        await newsRepository.DeleteAsync(newsItem, ct);

        if (newsItem.Photo != null)
        {
            await blobRepository.DeleteAllFilesByNameAsync("storonnimv-photo", $"news-{id}", ct);
        }
    }
    
    //todo: update news item
    // public async Task UpdateNewsItemAsync(NewsItemAdditionRequest request, CancellationToken ct)
}