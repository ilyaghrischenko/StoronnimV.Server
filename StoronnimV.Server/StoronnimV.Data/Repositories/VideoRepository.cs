using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StoronnimV.Data.Repositories.Shared;
using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Enums;
using StoronnimV.Domain.Interfaces;
using StoronnimV.Domain.Interfaces.Shared;

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

    public async Task<object?> GetByIdAsNoTrackingAsync(long id)
    {
        _logger.LogInformation(
            $"Repository: VideoRepository Method: GetByIdAsNoTrackingAsync with id: {id} started at {DateTime.UtcNow}");

        await using var context = await _contextFactory.CreateDbContextAsync();

        var video = await context.Videos
            .AsNoTracking()
            .Select(v => new
            {
                Id = v.Id,
                Title = v.Title,
                Url = v.Url
            })
            .FirstOrDefaultAsync(x => x.Id == id);

        _logger.LogInformation(
            $"Repository: VideoRepository Method: GetByIdAsNoTrackingAsync with id: {id} ended at {DateTime.UtcNow}");

        return video;
    }

    public async Task<IEnumerable<object>?> GetAllAsync()
    {
        _logger.LogInformation($"Repository: VideoRepository Method: GetAllAsync started at {DateTime.UtcNow}");

        await using var context = await _contextFactory.CreateDbContextAsync();

        var videos = await context.Videos
            .AsNoTracking()
            .Select(v => new
            {
                Id = v.Id,
                Title = v.Title,
                Url = v.Url
            })
            .ToListAsync();

        _logger.LogInformation($"Repository: VideoRepository Method: GetAllAsync ended at {DateTime.UtcNow}");

        return videos;
    }

    #region Specific videos

    public async Task<object?> GetPromotionVideo(long id)
    {
        _logger.LogInformation($"Repository: VideoRepository Method: GetPromotionVideo with id: {id} started at {DateTime.UtcNow}");
        
        await using var context = await _contextFactory.CreateDbContextAsync();

        var video = await context.Videos
            .AsNoTracking()
            .Where(video => video.Type == VideoType.Promotion)
            .Select(v => new
            {
                Id = v.Id,
                Title = v.Title,
                Url = v.Url
            })
            .FirstOrDefaultAsync(x => x.Id == id);
        
        _logger.LogInformation($"Repository: VideoRepository Method: GetPromotionVideo with id: {id} ended at {DateTime.UtcNow}");

        return video;
    }
    #endregion

    public async Task<IEnumerable<object>?> GetForPageAsync(int page, int pageSize = 10, params object[] args)
    {
        _logger.LogInformation($"Repository: VideoRepository Method: GetForPageAsync with [page: {page}, pageSize: {pageSize}] started at {DateTime.UtcNow}");

        var type = (string)args[0];
        var typeEnum = (VideoType)Enum.Parse(typeof(VideoType), type);
        
        await using var context = await _contextFactory.CreateDbContextAsync();

        var videos = await context.Videos
            .AsNoTracking()
            .Where(video => video.Type == typeEnum)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(v => new
            {
                Id = v.Id,
                Title = v.Title,
                Url = v.Url
            })
            .ToListAsync();
        
        _logger.LogInformation($"Repository: VideoRepository Method: GetVideoByType with type: {type} ended at {DateTime.UtcNow}");

        return videos;
    }

    public async Task<int> GetTotalCountAsync(params object[] args)
    {
        _logger.LogInformation($"Repository: VideoRepository Method: GetTotalCountAsync started at {DateTime.UtcNow}");
        
        var type = (string)args[0];
        var typeEnum = (VideoType)Enum.Parse(typeof(VideoType), type);
        
        await using var context = await _contextFactory.CreateDbContextAsync();

        var count = await context.Videos
            .AsNoTracking()
            .Where(video => video.Type == typeEnum)
            .CountAsync();
        
        _logger.LogInformation($"Repository: VideoRepository Method: GetTotalCountAsync ended at {DateTime.UtcNow}");

        return count;
    }
}