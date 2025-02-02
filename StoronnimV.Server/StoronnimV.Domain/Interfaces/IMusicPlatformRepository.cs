using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Interfaces.Shared;
using StoronnimV.Domain.Projections;

namespace StoronnimV.Domain.Interfaces;

public interface IMusicPlatformRepository
    : IRepository<MusicPlatform>, IGetByIdRepository<MusicPlatformProjection>, IGetAllRepository<MusicPlatformProjection>
{
    
}