using StoronnimV.Domain.Entities.Shared;

namespace StoronnimV.Domain.Entities;

public class Video : BaseEntity
{
    public string Url { get; set; }
    public string Title { get; set; }
    
    public Video() {}
    
    public Video(string url, string title)
    {
        Url = url;
        Title = title;
    }
}