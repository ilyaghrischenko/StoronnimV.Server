using Microsoft.Extensions.Logging;
using StoronnimV.Application.Exceptions;
using StoronnimV.Application.Interfaces.Entities;
using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Interfaces;

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
    
    public async Task<IEnumerable<object>> GetAllAsync()
    {
        _logger.LogInformation($"Service: MemberService Method: GetAllAsync started at {DateTime.UtcNow}");
        
        var members = await _memberRepository.GetAllAsync();
        
        _logger.LogInformation($"Service: MemberService Method: GetAllAsync ended at {DateTime.UtcNow}");

        return members ?? new List<object>();
    }

    public async Task<object> GetItemByIdAsync(long id)
    {
        _logger.LogInformation($"Service: MemberService Method: GetItemByIdAsync with id: {id} started at {DateTime.UtcNow}");
        
        var member = await _memberRepository.GetByIdAsNoTrackingAsync(id)
            ?? throw new EntityNotFoundException($"Member with id: {id} was not found");

        _logger.LogInformation($"Service: MemberService Method: GetItemByIdAsync with id: {id} ended at {DateTime.UtcNow}");

        return member;
    }
}