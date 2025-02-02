using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Interfaces.Shared;
using StoronnimV.Domain.Projections;

namespace StoronnimV.Domain.Interfaces;

public interface IGroupPageRepository
    : IRepository<GroupPage>, IGetByIdRepository<GroupPageProjection>, IGetAllRepository<GroupPageProjection>
{
    public Task<object?> GetFirstGroupPageAsync(CancellationToken ct);
}