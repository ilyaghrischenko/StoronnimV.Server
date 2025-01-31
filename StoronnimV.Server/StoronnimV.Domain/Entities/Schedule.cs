using StoronnimV.Domain.Entities.Shared;
using StoronnimV.Domain.Enums;

namespace StoronnimV.Domain.Entities;

/// <summary>
/// Сущность, для хранения информации об афишах
/// </summary>
public class Schedule : BaseEntity
{
    public string? Photo { get; set; }
    public string Title { get; set; }
    public DateTime PerformanceDateTime { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public ScheduleStatus Status { get; set; }
    
    private Schedule() {}

    public Schedule(string title, DateTime performanceDateTime, 
        string description, string location, ScheduleStatus status = ScheduleStatus.Active, string? photo = null)
    {
        Photo = photo;
        Title = title;
        PerformanceDateTime = performanceDateTime;
        Description = description;
        Location = location;
        Status = status;
    }
}