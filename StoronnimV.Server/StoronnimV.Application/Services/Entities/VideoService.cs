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
    IVideoRepository videoRepository) : IVideoService
{
    private readonly IVideoRepository _videoRepository = videoRepository;

    public async Task<VideoShortProjection> GetItemByIdAsync(long id, CancellationToken ct)
    {
        VideoShortProjection video = await _videoRepository.GetByIdAsNoTrackingAsync(id, ct)
                                     ?? throw new EntityNotFoundException($"Video with {nameof(id)}: {id} was not found");

        return video;
    }

    public async Task<PaginationResult<VideoShortProjection>> GetForPageAsync(int page, int pageSize, CancellationToken ct, params object[] args)
    {
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
    }
}