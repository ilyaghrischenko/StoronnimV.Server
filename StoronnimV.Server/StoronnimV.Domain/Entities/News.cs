using StoronnimV.Domain.Entities.Shared;
using StoronnimV.Domain.Enums;

namespace StoronnimV.Domain.Entities;

/// <summary>
/// Сущность, для хранения информации о новости
/// </summary>
public class News : BaseEntity
{
    public string? Photo { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Video? Video { get; set; }
    public NewsPriority Priority { get; set; }
    public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
    
    public News() {}
    public News(string title, string description, NewsPriority priority, string? photo = null, Video? video = null)
    {
        Photo = photo;
        Title = title;
        Description = description;
        Video = video;
        Priority = priority;
    }
}