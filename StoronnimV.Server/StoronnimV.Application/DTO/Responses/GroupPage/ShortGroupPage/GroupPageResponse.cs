using StoronnimV.Application.DTO.Responses.Shared;

namespace StoronnimV.Application.DTO.Responses.GroupPage.ShortGroupPage;

public class GroupPageResponse : BaseResponseDto
{
    public string PhotoUrl { get; set; }
    public string Description { get; set; }
    
    public GroupPageResponse() { }
    
    public GroupPageResponse(long id, string photoUrl, string description)
    {
        Id = id;
        PhotoUrl = photoUrl;
        Description = description;
    }
}