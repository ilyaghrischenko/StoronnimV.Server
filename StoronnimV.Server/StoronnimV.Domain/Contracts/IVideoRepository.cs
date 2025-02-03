using StoronnimV.Domain.Contracts.Shared;
using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Projections.Video;

namespace StoronnimV.Domain.Contracts;

public interface IVideoRepository 
    : IRepository<Video>, IGetByIdRepository<VideoShortProjection>,
        IPaginationRepository<VideoShortProjection>, IAdminPaginationRepository<VideoFullProjection>,
        IReceivableByTitleRepository<VideoFullProjection>
{
    public Task<VideoShortProjection?> GetPromotionVideoForHomePageAsync(CancellationToken ct);
    public Task<int> GetTotalCountForAdminPageAsync(CancellationToken ct);
}