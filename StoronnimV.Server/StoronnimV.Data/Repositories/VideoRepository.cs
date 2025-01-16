using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StoronnimV.Data.Repositories.Shared;
using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Interfaces;

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
}