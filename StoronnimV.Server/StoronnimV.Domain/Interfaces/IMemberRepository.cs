using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Interfaces.Shared;
using StoronnimV.Domain.Projections.Member;

namespace StoronnimV.Domain.Interfaces;

public interface IMemberRepository
    : IRepository<Member>, IGetByIdRepository<MemberFullProjection>, IGetAllRepository<MemberShortProjection>
{
}