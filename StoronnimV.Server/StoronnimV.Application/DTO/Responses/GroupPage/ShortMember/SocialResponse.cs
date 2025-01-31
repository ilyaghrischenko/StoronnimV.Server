using StoronnimV.Application.DTO.Responses.Shared;

namespace StoronnimV.Application.DTO.Responses.GroupPage.ShortMember;

public class SocialResponse : BaseResponseDto
{
    public required string SocialNetwork { get; init; }
    public required string Url { get; init; }
}