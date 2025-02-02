using StoronnimV.Application.Interfaces.Entities.Shared;
using StoronnimV.Domain.Projections;

namespace StoronnimV.Application.Interfaces.Entities;

public interface IGroupPageService : IGetByIdService<GroupPageProjection>, IGetAllService<GroupPageProjection>
{
    public Task<GroupPageProjection> GetFirstGroupPageAsync(CancellationToken ct);
}