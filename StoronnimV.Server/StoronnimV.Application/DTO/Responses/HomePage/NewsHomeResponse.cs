using StoronnimV.Application.DTO.Responses.Shared;

namespace StoronnimV.Application.DTO.Responses.HomePage;

public class NewsHomeResponse : BaseResponseDto
{
    public string Photo { get; set; }
    public string Title { get; set; }
    
    public NewsHomeResponse() { }

    public NewsHomeResponse(long id, string title, string photo)
    {
        Id = id;
        Title = title;
        Photo = photo;
    }
}