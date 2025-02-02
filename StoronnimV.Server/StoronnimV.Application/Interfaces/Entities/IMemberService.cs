using StoronnimV.Application.Interfaces.Entities.Shared;
using StoronnimV.Domain.Projections.Member;

namespace StoronnimV.Application.Interfaces.Entities;

public interface IMemberService : IGetByIdService<MemberFullProjection>, IGetAllService<MemberShortProjection>
{
    
}