using StoronnimV.Application.Contracts.Entities.Shared;
using StoronnimV.Application.DTO.Requests.Entities.Pages.Addition;
using StoronnimV.Domain.Projections;

namespace StoronnimV.Application.Contracts.Entities;

public interface IGroupPageService
    : IGetByIdService<GroupPageProjection>, IGetAllService<GroupPageProjection>
{
    Task<GroupPageProjection> GetFirstGroupPageAsync(CancellationToken ct);
    Task AddGroupPageAsync(GroupPageAdditionRequest request, CancellationToken ct);
    Task DeleteGroupPageAsync(long id, CancellationToken ct);
    
    //todo: update group page
    // Task UpdateGroupPageAsync(GroupPageUpdateRequest request, CancellationToken ct);
}