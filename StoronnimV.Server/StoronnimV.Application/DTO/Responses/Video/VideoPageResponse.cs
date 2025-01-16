using StoronnimV.Application.DTO.Responses.Shared;

namespace StoronnimV.Application.DTO.Responses.Video;

public class VideoPageResponse : BaseResponseDto
{
    public string Title { get; set; }
    public string Url { get; set; }
    
    public VideoPageResponse() {}

    public VideoPageResponse(long id, string title, string url)
    {
        Id = id;
        Title = title;
        Url = url;
    }
}