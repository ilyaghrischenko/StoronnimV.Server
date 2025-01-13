using StoronnimV.Application.DTO.Responses.Shared;

namespace StoronnimV.Application.DTO.Responses.SchedulePage;

public class ScheduleResponse : BaseResponseDto
{
    public string? Photo  { get; set; }
    public string Title  { get; set; }
    public string PerformanceDateTime  { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public string Status { get; set; }
    
    public ScheduleResponse() {}

    public ScheduleResponse(long id, string title, string performanceDateTime,
        string description, string location, string status, string? photo)
    {
        Id = id;
        Photo = photo;
        Title = title;
        PerformanceDateTime = performanceDateTime;
        Description = description;
        Location = location;
        Status = status;
    }
}