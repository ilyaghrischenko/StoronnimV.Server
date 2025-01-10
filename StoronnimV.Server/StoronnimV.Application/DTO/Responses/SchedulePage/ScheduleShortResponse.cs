using StoronnimV.Application.DTO.Responses.Shared;

namespace StoronnimV.Application.DTO.Responses.SchedulePage;

public class ScheduleShortResponse : BaseResponseDto
{
    public string Photo  { get; set; }
    public string Title  { get; set; }
    public string PerformanceDateTime  { get; set; }
    public string Location { get; set; }
    public string Status { get; set; }
    
    public ScheduleShortResponse() {}

    public ScheduleShortResponse(long id, string photo, string title,
        string performanceDateTime, string location, string status)
    {
        Id = id;
        Photo = photo;
        Title = title;
        PerformanceDateTime = performanceDateTime;
        Location = location;
        Status = status;
    }
}