using StoronnimV.Application.Contracts.Entities.Shared;
using StoronnimV.Domain.Projections;

namespace StoronnimV.Application.Contracts.Entities;

public interface IMusicPlatformService
    : IGetByIdService<MusicPlatformProjection>, IGetAllService<MusicPlatformProjection>
{
    
}