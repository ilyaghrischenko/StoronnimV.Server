using StoronnimV.Application.Contracts.Entities.Shared;
using StoronnimV.Domain.Projections.Video;

namespace StoronnimV.Application.Contracts.Entities;

public interface IVideoService 
    : IGetByIdService<VideoShortProjection>, IAdminPaginationService<VideoFullProjection>,
        IPaginationService<VideoShortProjection>, IReceivableByTitleService<VideoFullProjection>
{
    
}