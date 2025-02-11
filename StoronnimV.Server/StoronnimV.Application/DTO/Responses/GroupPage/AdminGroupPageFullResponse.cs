using StoronnimV.Application.DTO.Responses.GroupPage.ShortGroupPage;

namespace StoronnimV.Application.DTO.Responses.GroupPage;

public class AdminGroupPageFullResponse
{
    public required GroupPageResponse GroupPage { get; init; }
    public required IEnumerable<MemberFullInfoResponse> Members { get; init; }
}