using StoronnimV.Application.DTO.Responses.Shared;

namespace StoronnimV.Application.Interfaces.Controllers.Shared;

public interface IPaginationControllerService<TDto> where TDto : BaseDto
{
    Task<IEnumerable<TDto>> GetForPageAsync(int page);
}