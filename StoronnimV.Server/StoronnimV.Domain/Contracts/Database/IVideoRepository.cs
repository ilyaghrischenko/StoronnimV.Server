using StoronnimV.Domain.Contracts.Database.Shared;
using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Projections.Video;

namespace StoronnimV.Domain.Contracts.Database;

public interface IVideoRepository 
    : IRepository<Video>, IGetByIdRepository<VideoShortProjection>,
        IPaginationRepository<VideoShortProjection>
{
    public Task<VideoShortProjection?> GetPromotionVideoForHomePageAsync(CancellationToken ct);
}