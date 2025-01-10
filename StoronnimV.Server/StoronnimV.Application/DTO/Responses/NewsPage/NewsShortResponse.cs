using StoronnimV.Application.DTO.Responses.Shared;

namespace StoronnimV.Application.DTO.Responses.NewsPage;

public class NewsShortResponse : BaseResponseDto
{
    public string? Photo { get; set; }
    public string Title { get; set; }
    public string Priority { get; set; }
    public string Date { get; set; }
    
    public NewsShortResponse() { }
    
    public NewsShortResponse(long id, string title, string priority, string date, string? photo = null)
    {
        Id = id;
        Photo = photo;
        Title = title;
        Priority = priority;
        Date = date;
    }
}