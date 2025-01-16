using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Interfaces.Shared;

namespace StoronnimV.Domain.Interfaces;

public interface IVideoRepository 
    : IRepository<Video>, IReceivableRepository<Video>
{
    
}