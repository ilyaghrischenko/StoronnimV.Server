using StoronnimV.Application.Interfaces.Entities.Shared;
using StoronnimV.Domain.Projections.Video;

namespace StoronnimV.Application.Interfaces.Entities;

public interface IVideoService 
    : IGetByIdService<VideoShortProjection>, IAdminPaginationService<VideoFullProjection>, IPaginationService<VideoShortProjection>
{
    
}