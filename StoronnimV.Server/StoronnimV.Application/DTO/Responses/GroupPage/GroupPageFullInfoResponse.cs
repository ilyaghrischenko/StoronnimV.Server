using StoronnimV.Application.DTO.Responses.GroupPage.ShortGroupPage;
using StoronnimV.Application.DTO.Responses.Shared;

namespace StoronnimV.Application.DTO.Responses.GroupPage;

public class GroupPageFullInfoResponse
{
    public required GroupPageResponse GroupPage { get; init; }
    public required IEnumerable<MemberShortResponse> Members { get; init; }
}