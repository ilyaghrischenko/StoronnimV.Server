using StoronnimV.Domain.Entities.Shared;

namespace StoronnimV.Domain.Entities;

public class MusicPlatform : BaseEntity
{
    public string BgImageUrl { get; set; }
    public string PlatformUrl { get; set; }
    
    public MusicPlatform() {}

    public MusicPlatform(string bgImageUrl, string platformUrl)
    {
        BgImageUrl = bgImageUrl;
        PlatformUrl = platformUrl;
    }
}