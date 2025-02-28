using StoronnimV.Application.DTO.Requests.Entities.Pages.Editing.Shared;

namespace StoronnimV.Application.DTO.Requests.Entities.Pages.Editing;

public class GroupPageEditRequest : BaseEditRequest
{
    public string Description { get; set; } = string.Empty;
    
    public GroupPageEditRequest(){}
}