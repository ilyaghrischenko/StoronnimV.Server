using StoronnimV.Application.DTO.Responses.GroupPage.ShortMember;

namespace StoronnimV.Application.DTO.Responses.GroupPage;

public class MemberFullInfoResponse
{
    public required MemberResponse Member { get; init; }
    public required IEnumerable<SocialResponse> Socials { get; init; }
}