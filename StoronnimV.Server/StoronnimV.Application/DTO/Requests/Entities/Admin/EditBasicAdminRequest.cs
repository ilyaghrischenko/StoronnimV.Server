namespace StoronnimV.Application.DTO.Requests.Entities.Admin;

//TODO: ДОПИСАТЬ МОДЕЛЬ ЗАПРОСА НА ИЗМЕНЕНИЕ ДАННЫХ ОБЫЧНОГО АДМИНА И ПРОПИСАТЬ ДЛЯ ЭТОГО ВСЮ ЛОГИКУ
public class EditBasicAdminRequest
{
    public required string NewLogin { get; init; }
    public required string OldPassword { get; init; }
    public required string NewPassword { get; init; }
    public required string ConfirmNewPassword { get; init; }
}