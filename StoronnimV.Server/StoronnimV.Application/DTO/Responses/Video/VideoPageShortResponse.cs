using StoronnimV.Application.DTO.Responses.Shared;

namespace StoronnimV.Application.DTO.Responses.Video;

public class VideoPageShortResponse : BaseResponseDto
{
    public string Title { get; set; }
    public string Url { get; set; }
    
    public VideoPageShortResponse() {}

    public VideoPageShortResponse(long id, string title, string url)
    {
        Id = id;
        Title = title;
        Url = url;
    }
}