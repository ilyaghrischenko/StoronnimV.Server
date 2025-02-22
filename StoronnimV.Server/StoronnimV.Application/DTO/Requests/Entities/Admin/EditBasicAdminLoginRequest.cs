namespace StoronnimV.Application.DTO.Requests.Entities.Admin;

public class EditBasicAdminLoginRequest
{
    public required long Id { get; init; }
    public required string NewLogin { get; init; }
}