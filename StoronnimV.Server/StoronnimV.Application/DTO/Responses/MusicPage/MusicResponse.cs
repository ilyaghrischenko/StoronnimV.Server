using StoronnimV.Application.DTO.Responses.Shared;

namespace StoronnimV.Application.DTO.Responses.MusicPage;

public class MusicResponse : BaseResponseDto
{
    public string BgImageUrl { get; set; }
    public string PlatformUrl { get; set; }
    
    public MusicResponse() {}

    public MusicResponse(long id, string bgImageUrl, string platformUrl)
    {
        Id = id;
        BgImageUrl = bgImageUrl;
        PlatformUrl = platformUrl;
    }
}