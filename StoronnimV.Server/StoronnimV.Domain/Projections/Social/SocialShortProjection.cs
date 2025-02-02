using StoronnimV.Domain.Enums;
using StoronnimV.Domain.Projections.Shared;

namespace StoronnimV.Domain.Projections.Social;

public class SocialShortProjection : BaseProjection
{
    public required SocialType Type { get; init; }
    public required string Url { get; init; }
}