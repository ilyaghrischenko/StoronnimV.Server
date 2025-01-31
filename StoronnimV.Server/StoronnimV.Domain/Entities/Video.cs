using StoronnimV.Domain.Entities.Shared;
using StoronnimV.Domain.Enums;

namespace StoronnimV.Domain.Entities;

public class Video : BaseEntity
{
    public string Url { get; set; }
    public string Title { get; set; }
    public VideoType Type { get; set; }
    
    private Video() {}
    
    public Video(string url, string title, VideoType type)
    {
        Url = url;
        Title = title;
        Type = type;
    }
}