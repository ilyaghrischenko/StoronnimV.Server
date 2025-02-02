using StoronnimV.Domain.Contracts.Shared;
using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Projections;

namespace StoronnimV.Domain.Contracts;

public interface IMusicPlatformRepository
    : IRepository<MusicPlatform>, IGetByIdRepository<MusicPlatformProjection>, IGetAllRepository<MusicPlatformProjection>
{
    
}