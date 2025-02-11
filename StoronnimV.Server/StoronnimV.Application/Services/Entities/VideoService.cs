using Microsoft.Extensions.Logging;
using StoronnimV.Application.Contracts.Entities;
using StoronnimV.Application.Exceptions;
using StoronnimV.Application.Models;
using StoronnimV.Domain.Contracts;
using StoronnimV.Domain.Contracts.Database;
using StoronnimV.Domain.Enums;
using StoronnimV.Domain.Projections.Video;

namespace StoronnimV.Application.Services.Entities;

public class VideoService(
    IVideoRepository videoRepository,
    ILogger<VideoService> logger) : IVideoService
{
    private readonly IVideoRepository _videoRepository = videoRepository;
    private readonly ILogger<VideoService> _logger = logger;

    public async Task<VideoShortProjection> GetItemByIdAsync(long id, CancellationToken ct)
    {
        _logger.LogInformation(
            $"Service: VideoService Method: GetItemByIdAsync with id: {id} started at {DateTime.UtcNow}");

        VideoShortProjection video = await _videoRepository.GetByIdAsNoTrackingAsync(id, ct)
                                     ?? throw new EntityNotFoundException($"Video with id: {id} was not found");

        _logger.LogInformation(
            $"Service: VideoService Method: GetItemByIdAsync with id: {id} ended at {DateTime.UtcNow}");

        return video;
    }

    public async Task<PaginationResult<VideoShortProjection>> GetForPageAsync(int page, int pageSize, CancellationToken ct, params object[] args)
    {
        _logger.LogInformation(
            $"Service: VideoService Method: GetForPageAsync with [page: {page}, pageSize: {pageSize}] started at {DateTime.UtcNow}");

        string type = (string)args[0];
        
        if (page <= 0)
        {
            throw new PaginationException("invalid page number");
        }

        int totalCount = await _videoRepository.GetTotalCountAsync(ct, type);

        try
        {
            if (totalCount == 0)
            {
                throw new PaginationException(string.Empty);
            }

            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var items = await _videoRepository.GetForPageAsync(page, ct, pageSize, type);

            if (items is null || !items.Any())
            {
                throw new PaginationException(string.Empty);
            }

            var sortedItems = items.ToList();

            PaginationResult<VideoShortProjection> paginationResult = new()
            {
                CurrentPage = page,
                TotalPages = totalPages,
                TotalItems = totalCount,
                Items = sortedItems
            };

            return paginationResult;
        }
        catch (PaginationException)
        {
            return new PaginationResult<VideoShortProjection>
            {
                CurrentPage = page,
                TotalPages = 0,
                TotalItems = 0,
                Items = []
            };
        }
        finally
        {
            _logger.LogInformation($"Service: VideoService Method: GetForPageAsync with [page: {page}, pageSize: {pageSize}] ended at {DateTime.UtcNow}");
        }
    }

    public async Task<PaginationResult<VideoFullProjection>> GetForAdminPageAsync(int page, int pageSize, CancellationToken ct, params object[] args)
    {
        _logger.LogInformation($"Service: VideoService Method: GetForAdminPageAsync with [page: {page}, pageSize: {pageSize}] started at {DateTime.UtcNow}");
        
        if (page <= 0)
        {
            throw new PaginationException("invalid page number");
        }

        int totalCount = await _videoRepository.GetTotalCountForAdminPageAsync(ct);

        try
        {
            if (totalCount == 0)
            {
                throw new PaginationException(string.Empty);
            }

            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var items = await _videoRepository.GetForAdminPageAsync(page, ct, pageSize);

            if (items is null || !items.Any())
            {
                throw new PaginationException(string.Empty);
            }

            var sortedItems = items.ToList();

            PaginationResult<VideoFullProjection> paginationResult = new()
            {
                CurrentPage = page,
                TotalPages = totalPages,
                TotalItems = totalCount,
                Items = sortedItems
            };

            return paginationResult;
        }
        catch (PaginationException)
        {
            return new PaginationResult<VideoFullProjection>
            {
                CurrentPage = page,
                TotalPages = 0,
                TotalItems = 0,
                Items = []
            };
        }
        finally
        {
            _logger.LogInformation(
                $"Service: VideoService Method: GetForAdminPageAsync with [page: {page}, pageSize: {pageSize}] ended at {DateTime.UtcNow}");
        }
    }

    public async Task<IEnumerable<VideoFullProjection>> GetItemsByTitleAsync(string title, CancellationToken ct)
    {
        _logger.LogInformation($"Service: VideoService Method: GetItemsByTitleAsync with title: {title} started at {DateTime.UtcNow}");

        string formattedTitle = title.Trim().ToLower();
        
        var projectionsByTitle = await _videoRepository.GetItemsByTitle(formattedTitle, ct);

        if (projectionsByTitle is null || !projectionsByTitle.Any())
        {
            return new List<VideoFullProjection>();
        }
        
        _logger.LogInformation($"Service: VideoService Method: GetItemsByTitleAsync with title: {title} ended at {DateTime.UtcNow}");

        return projectionsByTitle;
    }
}