using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StoronnimV.Data.Repositories.Shared;
using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Enums;
using StoronnimV.Domain.Interfaces;
using StoronnimV.Domain.Interfaces.Shared;
using StoronnimV.Domain.Projections.Video;

namespace StoronnimV.Data.Repositories;

/// <summary>
/// Репозиторий для получения данных напрямую с бд
/// </summary>
/// <param name="contextFactory"></param>
/// <param name="logger"></param>
public class VideoRepository(
    IDbContextFactory<StoronnimVContext> contextFactory,
    ILogger<VideoRepository> logger)
    : Repository<Video>(contextFactory), IVideoRepository
{
    private readonly IDbContextFactory<StoronnimVContext> _contextFactory = contextFactory;
    private readonly ILogger<VideoRepository> _logger = logger;

    public async Task<object?> GetByIdAsNoTrackingAsync(long id, CancellationToken ct)
    {
        _logger.LogInformation(
            $"Repository: VideoRepository Method: GetByIdAsNoTrackingAsync with id: {id} started at {DateTime.UtcNow}");

        await using StoronnimVContext context = await _contextFactory.CreateDbContextAsync(ct);

        VideoShortProjection? video = await context.Videos
            .AsNoTracking()
            .Select(v => new VideoShortProjection
            {
                Id = v.Id,
                Title = v.Title,
                Url = v.Url
            })
            .FirstOrDefaultAsync(x => x.Id == id, ct);

        _logger.LogInformation(
            $"Repository: VideoRepository Method: GetByIdAsNoTrackingAsync with id: {id} ended at {DateTime.UtcNow}");

        return video;
    }

    public async Task<IEnumerable<object>?> GetAllAsync(CancellationToken ct)
    {
        _logger.LogInformation($"Repository: VideoRepository Method: GetAllAsync started at {DateTime.UtcNow}");

        await using StoronnimVContext context = await _contextFactory.CreateDbContextAsync(ct);

        var videos = await context.Videos
            .AsNoTracking()
            .Select(v => new VideoShortProjection
            {
                Id = v.Id,
                Title = v.Title,
                Url = v.Url
            })
            .ToListAsync(ct);

        _logger.LogInformation($"Repository: VideoRepository Method: GetAllAsync ended at {DateTime.UtcNow}");

        return videos;
    }

    public async Task<object?> GetPromotionVideoForHomePageAsync(CancellationToken ct)
    {
        _logger.LogInformation($"Repository: VideoRepository Method: GetPromotionVideoForHomePageAsync started at {DateTime.UtcNow}");
        
        await using StoronnimVContext context = await _contextFactory.CreateDbContextAsync(ct);

        VideoShortProjection? promotionVideo = await context.Videos
            .AsNoTracking()
            .Where(video => video.Type == VideoType.Promotion)
            .Select(video => new VideoShortProjection
            {
                Id = video.Id,
                Title = video.Title,
                Url = video.Url
            })
            .FirstOrDefaultAsync(ct);
        
        _logger.LogInformation($"Repository: VideoRepository Method: GetPromotionVideoForHomePageAsync ended at {DateTime.UtcNow}");

        return promotionVideo;
    }


    public async Task<IEnumerable<object>?> GetForPageAsync(int page, CancellationToken ct, int pageSize = 10, params object[] args)
    {
        _logger.LogInformation($"Repository: VideoRepository Method: GetForPageAsync with [page: {page}, pageSize: {pageSize}] started at {DateTime.UtcNow}");

        string type = (string)args[0];
        var typeEnum = Enum.Parse<VideoType>(type);
        
        await using StoronnimVContext context = await _contextFactory.CreateDbContextAsync(ct);

        var videos = await context.Videos
            .AsNoTracking()
            .Where(video => video.Type == typeEnum)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(v => new VideoShortProjection
            {
                Id = v.Id,
                Title = v.Title,
                Url = v.Url
            })
            .ToListAsync(ct);
        
        _logger.LogInformation($"Repository: VideoRepository Method: GetVideoByType with type: {type} ended at {DateTime.UtcNow}");

        return videos;
    }

    public async Task<int> GetTotalCountAsync(CancellationToken ct, params object[] args)
    {
        _logger.LogInformation($"Repository: VideoRepository Method: GetTotalCountAsync started at {DateTime.UtcNow}");
        
        string type = (string)args[0];
        var typeEnum = Enum.Parse<VideoType>(type);
        
        await using StoronnimVContext context = await _contextFactory.CreateDbContextAsync(ct);

        int count = await context.Videos
            .AsNoTracking()
            .Where(video => video.Type == typeEnum)
            .CountAsync(ct);
        
        _logger.LogInformation($"Repository: VideoRepository Method: GetTotalCountAsync ended at {DateTime.UtcNow}");

        return count;
    }

    public async Task<IEnumerable<object>?> GetForAdminPageAsync(int page, CancellationToken ct, int pageSize = 10, params object[] args)
    {
        _logger.LogInformation($"Repository: VideoRepository Method: GetForAdminPageAsync with [page: {page}, pageSize: {pageSize}] started at {DateTime.UtcNow}");
        
        await using StoronnimVContext context = await _contextFactory.CreateDbContextAsync(ct);

        var videos = await context.Videos
            .AsNoTracking()
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(v => new VideoFullProjection
            {
                Id = v.Id,
                Title = v.Title,
                Url = v.Url,
                Type = v.Type
            })
            .ToListAsync(ct);
        
        _logger.LogInformation($"Repository: VideoRepository Method: GetForAdminPageAsync with [page: {page}, pageSize: {pageSize}] ended at {DateTime.UtcNow}");

        return videos;
    }

    public async Task<int> GetTotalCountForAdminPageAsync(CancellationToken ct)
    {
        _logger.LogInformation($"Repository: VideoRepository Method: GetTotalCountForAdminPageAsync started at {DateTime.UtcNow}");
        
        await using StoronnimVContext context = await _contextFactory.CreateDbContextAsync(ct);

        int count = await context.Videos
            .AsNoTracking()
            .CountAsync(ct);
        
        _logger.LogInformation($"Repository: VideoRepository Method: GetTotalCountForAdminPageAsync ended at {DateTime.UtcNow}");

        return count;
    }
}