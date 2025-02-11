using StoronnimV.Domain.Projections.Shared;

namespace StoronnimV.Domain.Projections.Member;

public class MemberWithSocialsProjection : BaseProjection
{
    public required MemberFullProjection Member { get; init; }
    public required IEnumerable<SocialProjection> Socials { get; init; }
}