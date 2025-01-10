using StoronnimV.Application.DTO.Responses.Shared;

namespace StoronnimV.Application.Interfaces.Controllers.Shared;

public interface IPaginationControllerService<TDto> where TDto : BaseResponseDto
{
    Task<TDto> GetForPageAsync(int page);
}