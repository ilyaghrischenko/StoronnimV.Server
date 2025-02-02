using Microsoft.Extensions.Logging;
using StoronnimV.Application.Exceptions;
using StoronnimV.Application.Interfaces.Entities;
using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Interfaces;
using StoronnimV.Domain.Projections.Member;

namespace StoronnimV.Application.Services.Entities;

/// <summary>
/// Сервис для проверки полученных данных, полученых с репозитория
/// </summary>
/// <param name="memberRepository"></param>
public class MemberService(IMemberRepository memberRepository,
    ILogger<MemberService> logger) : IMemberService
{
    private readonly IMemberRepository _memberRepository = memberRepository;
    private readonly ILogger<MemberService> _logger = logger;
    
    public async Task<IEnumerable<MemberShortProjection>> GetAllAsync(CancellationToken ct)
    {
        _logger.LogInformation($"Service: MemberService Method: GetAllAsync started at {DateTime.UtcNow}");
        
        var members = await _memberRepository.GetAllAsNoTrackingAsync(ct);
        
        _logger.LogInformation($"Service: MemberService Method: GetAllAsync ended at {DateTime.UtcNow}");

        return members ?? new List<MemberShortProjection>();
    }

    public async Task<MemberFullProjection> GetItemByIdAsync(long id, CancellationToken ct)
    {
        _logger.LogInformation($"Service: MemberService Method: GetItemByIdAsync with id: {id} started at {DateTime.UtcNow}");
        
        MemberFullProjection member = await _memberRepository.GetByIdAsNoTrackingAsync(id, ct)
                                      ?? throw new EntityNotFoundException($"Member with id: {id} was not found");

        _logger.LogInformation($"Service: MemberService Method: GetItemByIdAsync with id: {id} ended at {DateTime.UtcNow}");

        return member;
    }
}