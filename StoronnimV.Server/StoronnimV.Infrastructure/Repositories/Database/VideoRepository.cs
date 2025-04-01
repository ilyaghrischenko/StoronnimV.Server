using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StoronnimV.Domain.Contracts;
using StoronnimV.Domain.Contracts.Database;
using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Enums;
using StoronnimV.Domain.Projections.Video;
using StoronnimV.Infrastructure.Repositories.Database.Shared;

namespace StoronnimV.Infrastructure.Repositories.Database;

/// <summary>
/// Репозиторий для получения данных напрямую с бд
/// </summary>
/// <param name="contextFactory"></param>
/// <param name="logger"></param>
public class VideoRepository(IDbContextFactory<StoronnimVContext> contextFactory)
    : Repository<Video>(contextFactory), IVideoRepository
{
    private readonly IDbContextFactory<StoronnimVContext> _contextFactory = contextFactory;

    public async Task<VideoFullProjection?> GetByIdAsNoTrackingAsync(long id, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();

        await using StoronnimVContext context = await _contextFactory.CreateDbContextAsync(ct);

        VideoFullProjection? video = await context.Videos
            .AsNoTracking()
            .Select(v => new VideoFullProjection
            {
                Id = v.Id,
                Title = v.Title,
                Type = v.Type,
                Url = v.Url
            })
            .FirstOrDefaultAsync(x => x.Id == id, ct);

        return video;
    }

    public async Task<VideoFullProjection?> GetPromotionVideoForHomePageAsync(CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();

        await using StoronnimVContext context = await _contextFactory.CreateDbContextAsync(ct);

        VideoFullProjection? promotionVideo = await context.Videos
            .AsNoTracking()
            .Where(video => video.Type == VideoType.Promotion)
            .Select(video => new VideoFullProjection
            {
                Id = video.Id,
                Title = video.Title,
                Type = video.Type,
                Url = video.Url
            })
            .FirstOrDefaultAsync(ct);
        
        return promotionVideo;
    }


    public async Task<IEnumerable<VideoFullProjection>?> GetForPageAsync(int page, CancellationToken ct, int pageSize = 10, params object[] args)
    {
        ct.ThrowIfCancellationRequested();

        string type = (string)args[0];
        var typeEnum = Enum.Parse<VideoType>(type);
        
        await using StoronnimVContext context = await _contextFactory.CreateDbContextAsync(ct);

        var videos = await context.Videos
            .AsNoTracking()
            .Where(video => video.Type == typeEnum)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(v => new VideoFullProjection
            {
                Id = v.Id,
                Title = v.Title,
                Type = v.Type,
                Url = v.Url
            })
            .ToListAsync(ct);
        
        return videos;
    }

    public async Task<int> GetTotalCountAsync(CancellationToken ct, params object[] args)
    {
        ct.ThrowIfCancellationRequested();

        string type = (string)args[0];
        var typeEnum = Enum.Parse<VideoType>(type);
        
        await using StoronnimVContext context = await _contextFactory.CreateDbContextAsync(ct);

        int count = await context.Videos
            .AsNoTracking()
            .Where(video => video.Type == typeEnum)
            .CountAsync(ct);
        
        return count;
    }
}