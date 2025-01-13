using StoronnimV.Application.DTO.Responses.Shared;

namespace StoronnimV.Application.DTO.Responses.NewsPage;

public class NewsResponse : BaseResponseDto
{
    public string? Photo { get; set; }
    public string? Video { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Priority { get; set; }
    public string Date { get; set; }
    
    public NewsResponse() { }
    
    public NewsResponse(long id, string title, string description, string priority, string date, string? photo = null, string? video = null)
    {
        Id = id;
        Photo = photo;
        Video = video;
        Title = title;
        Description = description;
        Priority = priority;
        Date = date;
    }
}