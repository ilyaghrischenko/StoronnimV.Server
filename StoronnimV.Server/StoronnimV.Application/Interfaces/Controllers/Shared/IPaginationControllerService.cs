using StoronnimV.Application.DTO.Responses.Shared;

namespace StoronnimV.Application.Interfaces.Controllers.Shared;

public interface IPaginationControllerService<TDto>
{
    Task<TDto> GetForPageAsync(int page, int pageSize);
}