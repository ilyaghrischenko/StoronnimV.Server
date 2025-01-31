using StoronnimV.Application.DTO.Responses.Shared;

namespace StoronnimV.Application.DTO.Responses.Video;

public class VideoPageShortResponse : BaseResponseDto
{
    public required string Title { get; init; }
    public required string Url { get; init; }
}