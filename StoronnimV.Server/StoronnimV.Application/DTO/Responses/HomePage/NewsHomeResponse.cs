using StoronnimV.Application.DTO.Responses.Shared;

namespace StoronnimV.Application.DTO.Responses.HomePage;

public class NewsHomeResponse : BaseResponseDto
{
    public string Title { get; set; }
    public string Date { get; set; }
    
    public NewsHomeResponse() { }

    public NewsHomeResponse(long id, string title, string date)
    {
        Id = id;
        Title = title;
        Date = date;
    }
}