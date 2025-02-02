using StoronnimV.Application.Contracts.Entities.Shared;
using StoronnimV.Domain.Projections;

namespace StoronnimV.Application.Contracts.Entities;

public interface IGroupPageService : IGetByIdService<GroupPageProjection>, IGetAllService<GroupPageProjection>
{
    public Task<GroupPageProjection> GetFirstGroupPageAsync(CancellationToken ct);
}