using StoronnimV.Application.Contracts.Entities.Shared;
using StoronnimV.Application.DTO.Requests.Entities.Pages.Addition;
using StoronnimV.Domain.Projections.Member;

namespace StoronnimV.Application.Contracts.Entities;

public interface IMemberService
    : IGetByIdService<MemberFullProjection>, IGetAllService<MemberShortProjection>
{
    Task AddMemberAsync(MemberAdditionRequest request, CancellationToken ct);
    Task DeleteMemberAsync(long id, CancellationToken ct);
}