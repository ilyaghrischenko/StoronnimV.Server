using StoronnimV.Application.Interfaces.Entities.Shared;

namespace StoronnimV.Application.Interfaces.Entities;

public interface INewsService
    : IPaginationService, IAdminPaginationService, IReceivableService
{
    
}