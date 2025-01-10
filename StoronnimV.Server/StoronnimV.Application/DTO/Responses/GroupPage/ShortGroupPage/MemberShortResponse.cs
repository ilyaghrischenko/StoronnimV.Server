using StoronnimV.Application.DTO.Responses.Shared;

namespace StoronnimV.Application.DTO.Responses.GroupPage.ShortGroupPage;

public class MemberShortResponse : BaseResponseDto
{
    public string PhotoUrl { get; set; }
    public string FullName { get; set; }
    public string Role { get; set; }

    public MemberShortResponse()
    {
    }

    public MemberShortResponse(long id, string photoUrl, string fullName, string role)
    {
        Id = id;
        PhotoUrl = photoUrl;
        FullName = fullName;
        Role = role;
    }
}