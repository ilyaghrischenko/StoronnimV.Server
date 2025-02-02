using StoronnimV.Application.DTO.Responses.Shared;

namespace StoronnimV.Application.Interfaces.Controllers.Shared;

public interface IGetAllControllerService<TDto> where TDto : BaseResponseDto
{
    Task<IEnumerable<TDto>> GetAllAsync(CancellationToken ct);
}