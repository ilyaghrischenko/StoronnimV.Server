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

    public async Task<VideoShortProjection?> GetByIdAsNoTrackingAsync(long id, CancellationToken ct)
    {
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

        return video;
    }

    public async Task<VideoShortProjection?> GetPromotionVideoForHomePageAsync(CancellationToken ct)
    {
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
        
        return promotionVideo;
    }


    public async Task<IEnumerable<VideoShortProjection>?> GetForPageAsync(int page, CancellationToken ct, int pageSize = 10, params object[] args)
    {
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
        
        return videos;
    }

    public async Task<int> GetTotalCountAsync(CancellationToken ct, params object[] args)
    {
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