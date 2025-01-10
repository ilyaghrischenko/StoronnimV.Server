using StoronnimV.Application.DTO.Responses.GroupPage;

namespace StoronnimV.Application.Interfaces.Controllers;

public interface IGroupPageControllerService
{
    public Task<GroupPageFullInfoResponse> GetGroupPageInfoAsync();
    public Task<MemberFullInfoResponse> GetMemberInfoAsync(long memberId);
}