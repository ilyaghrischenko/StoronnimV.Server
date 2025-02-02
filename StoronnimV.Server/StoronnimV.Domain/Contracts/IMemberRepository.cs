using StoronnimV.Domain.Contracts.Shared;
using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Projections.Member;

namespace StoronnimV.Domain.Contracts;

public interface IMemberRepository
    : IRepository<Member>, IGetByIdRepository<MemberFullProjection>, IGetAllRepository<MemberShortProjection>
{
    
}