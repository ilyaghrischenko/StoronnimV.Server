using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Enums;
using StoronnimV.Domain.Interfaces.Shared;
using StoronnimV.Domain.Projections.Video;

namespace StoronnimV.Domain.Interfaces;

public interface IVideoRepository 
    : IRepository<Video>, IGetByIdRepository<VideoShortProjection>,
        IPaginationRepository<VideoShortProjection>, IAdminPaginationRepository<VideoFullProjection>
{
    public Task<object?> GetPromotionVideoForHomePageAsync(CancellationToken ct);
    public Task<int> GetTotalCountForAdminPageAsync(CancellationToken ct);
}