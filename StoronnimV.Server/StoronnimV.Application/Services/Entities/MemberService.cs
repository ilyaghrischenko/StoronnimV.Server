using Microsoft.Extensions.Logging;
using StoronnimV.Application.Contracts.Entities;
using StoronnimV.Application.Exceptions;
using StoronnimV.Domain.Contracts;
using StoronnimV.Domain.Contracts.Database;
using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Projections.Member;

namespace StoronnimV.Application.Services.Entities;

/// <summary>
/// Сервис для проверки полученных данных, полученых с репозитория
/// </summary>
/// <param name="memberRepository"></param>
public class MemberService(IMemberRepository memberRepository) : IMemberService
{
    private readonly IMemberRepository _memberRepository = memberRepository;
    
    public async Task<IEnumerable<MemberShortProjection>> GetAllAsync(CancellationToken ct)
    {
        var members = await _memberRepository.GetAllAsNoTrackingAsync(ct);
        
        return members ?? new List<MemberShortProjection>();
    }

    public async Task<MemberFullProjection> GetItemByIdAsync(long id, CancellationToken ct)
    {
        MemberFullProjection member = await _memberRepository.GetByIdAsNoTrackingAsync(id, ct)
                                      ?? throw new EntityNotFoundException($"Member with {nameof(id)}: {id} was not found");

        return member;
    }
}