using Microsoft.Extensions.Logging;
using StoronnimV.Application.Exceptions;
using StoronnimV.Application.Interfaces.Entities;
using StoronnimV.Application.Models;
using StoronnimV.Domain.Enums;
using StoronnimV.Domain.Interfaces;

namespace StoronnimV.Application.Services.Entities;

public class VideoService(
    IVideoRepository videoRepository,
    ILogger<VideoService> logger)
    : IVideoService
{
    private readonly IVideoRepository _videoRepository = videoRepository;
    private readonly ILogger<VideoService> _logger = logger;

    public async Task<object> GetItemByIdAsync(long id, CancellationToken ct)
    {
        _logger.LogInformation(
            $"Service: VideoService Method: GetItemByIdAsync with id: {id} started at {DateTime.UtcNow}");

        object video = await _videoRepository.GetByIdAsNoTrackingAsync(id, ct)
                       ?? throw new EntityNotFoundException($"Video with id: {id} was not found");

        _logger.LogInformation(
            $"Service: VideoService Method: GetItemByIdAsync with id: {id} ended at {DateTime.UtcNow}");

        return video;
    }

    public async Task<IEnumerable<object>> GetAllAsync(CancellationToken ct)
    {
        _logger.LogInformation($"Service: VideoService Method: GetAllAsync started at {DateTime.UtcNow}");

        var videos = await _videoRepository.GetAllAsync(ct);

        _logger.LogInformation($"Service: VideoService Method: GetAllAsync ended at {DateTime.UtcNow}");

        return videos ?? new List<object>();
    }


    public async Task<PaginationResult> GetForPageAsync(int page, int pageSize, CancellationToken ct, params object[] args)
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

            PaginationResult paginationResult = new()
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
            return new PaginationResult
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

    public async Task<PaginationResult> GetForAdminPageAsync(int page, int pageSize, CancellationToken ct, params object[] args)
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

            PaginationResult paginationResult = new()
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
            return new PaginationResult
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
}