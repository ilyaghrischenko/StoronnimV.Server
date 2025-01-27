using StoronnimV.Application.DTO.Responses.GroupPage;

namespace StoronnimV.Application.Interfaces.Controllers;

public interface IGroupPageControllerService
{
    public Task<GroupPageFullInfoResponse> GetGroupPageInfoAsync(CancellationToken ct);
    public Task<MemberFullInfoResponse> GetMemberInfoAsync(long memberId, CancellationToken ct);
}