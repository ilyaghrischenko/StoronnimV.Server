namespace StoronnimV.Domain.Interfaces.Shared;

public interface IAdminPaginationRepository
{
    Task<IEnumerable<object>?> GetForAdminPageAsync(int page, int pageSize = 10, params object[] args);
}