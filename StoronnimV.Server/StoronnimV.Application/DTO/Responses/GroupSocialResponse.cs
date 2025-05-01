using StoronnimV.Application.DTO.Responses.Shared;
using StoronnimV.Domain.Enums;

namespace StoronnimV.Application.DTO.Responses;

public class GroupSocialResponse : BaseResponseDto
{
    public required string PhotoUrl { get; init; }
    public required SocialType Name { get; init; }
    public required string LinkUrl { get; init; }
}