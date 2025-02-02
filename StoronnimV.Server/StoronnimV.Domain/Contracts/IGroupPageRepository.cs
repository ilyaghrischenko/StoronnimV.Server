using StoronnimV.Domain.Contracts.Shared;
using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Projections;

namespace StoronnimV.Domain.Contracts;

public interface IGroupPageRepository
    : IRepository<GroupPage>, IGetByIdRepository<GroupPageProjection>, IGetAllRepository<GroupPageProjection>
{
    public Task<GroupPageProjection?> GetFirstGroupPageAsync(CancellationToken ct);
}