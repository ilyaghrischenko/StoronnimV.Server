using StoronnimV.Application.Interfaces.Entities.Shared;
using StoronnimV.Domain.Projections;

namespace StoronnimV.Application.Interfaces.Entities;

public interface IMusicPlatformService
    : IGetByIdService<MusicPlatformProjection>, IGetAllService<MusicPlatformProjection>
{
    
}