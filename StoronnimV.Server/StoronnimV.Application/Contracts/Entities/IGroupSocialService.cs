using StoronnimV.Application.Contracts.Entities.Shared;
using StoronnimV.Domain.Projections;

namespace StoronnimV.Application.Contracts.Entities;

public interface IGroupSocialService
    : IGetByIdService<GroupSocialProjection>, IGetAllService<GroupSocialProjection>
{
    //TODO: admin methods (add, update, delete and for photo too)
}