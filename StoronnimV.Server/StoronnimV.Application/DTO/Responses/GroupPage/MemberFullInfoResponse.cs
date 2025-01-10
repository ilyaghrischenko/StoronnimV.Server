using StoronnimV.Application.DTO.Responses.GroupPage.ShortMember;

namespace StoronnimV.Application.DTO.Responses.GroupPage;

public class MemberFullInfoResponse
{
    public MemberResponse Member { get; set; }
    public IEnumerable<SocialResponse> Socials { get; set; }
    
    public MemberFullInfoResponse() { }
    
    public MemberFullInfoResponse(MemberResponse member, IEnumerable<SocialResponse> socials)
    {
        Member = member;
        Socials = socials;
    }
}