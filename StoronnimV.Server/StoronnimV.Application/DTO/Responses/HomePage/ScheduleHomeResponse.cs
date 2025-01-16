using StoronnimV.Application.DTO.Responses.Shared;

namespace StoronnimV.Application.DTO.Responses.HomePage;

public class ScheduleHomeResponse : BaseResponseDto
{
    public string Photo { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string PerformanceDateTime { get; set; }
    public string Location { get; set; }
    
    public ScheduleHomeResponse() {}

    public ScheduleHomeResponse(long id, string photo, string title, string description,
        string performanceDateTime, string location)
    {
        Id = id;
        Photo = photo;
        Title = title;
        Description = description;
        PerformanceDateTime = performanceDateTime;
        Location = location;
    }
}