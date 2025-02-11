using StoronnimV.Application.Contracts.Entities.Shared;
using StoronnimV.Domain.Projections.Member;

namespace StoronnimV.Application.Contracts.Entities;

public interface IMemberService
    : IGetByIdService<MemberFullProjection>, IGetAllService<MemberShortProjection>,
        IAdminGetAllService<MemberFullProjection>
{
    
}