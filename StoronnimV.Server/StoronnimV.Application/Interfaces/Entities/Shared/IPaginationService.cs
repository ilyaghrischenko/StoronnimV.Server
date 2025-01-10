namespace StoronnimV.Application.Interfaces.Entities.Shared;

public interface IPaginationService
{
    Task<IEnumerable<object>> GetForPageAsync(int page);
}