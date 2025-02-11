using Microsoft.Extensions.Logging;
using StoronnimV.Application.Contracts.Entities;
using StoronnimV.Application.Exceptions;
using StoronnimV.Domain.Contracts;
using StoronnimV.Domain.Projections;

namespace StoronnimV.Application.Services.Entities;

/// <summary>
/// Сервис для проверки полученных данных, полученых с репозитория
/// </summary>
/// <param name="socialRepository"></param>
public class SocialService(ISocialRepository socialRepository,
    ILogger<SocialService> logger) : ISocialService
{
    private readonly ISocialRepository _socialRepository = socialRepository;
    private readonly ILogger<SocialService> _logger = logger;
    
    public async Task<SocialProjection> GetItemByIdAsync(long id, CancellationToken ct)
    {
        _logger.LogInformation($"Service: SocialService Method: GetItemByIdAsync with id: {id} started at {DateTime.UtcNow}");
        
        SocialProjection social = await _socialRepository.GetByIdAsNoTrackingAsync(id, ct)
                                       ?? throw new EntityNotFoundException($"Social with id: {id} was not found");
        
        _logger.LogInformation($"Service: SocialService Method: GetItemByIdAsync with id: {id} ended at {DateTime.UtcNow}");

        return social;
    }
    
    public async Task<IEnumerable<SocialProjection>> GetAllForMemberAsync(long memberId, CancellationToken ct)
    {
        _logger.LogInformation($"Service: SocialService Method: GetAllForMemberAsync with memberId: {memberId} started at {DateTime.UtcNow}");
        
        var socials = await _socialRepository.GetAllForMemberAsync(memberId, ct)
                                        ?? throw new EntityNotFoundException($"Socials with member id: {memberId} was not found");
        
        _logger.LogInformation($"Service: SocialService Method: GetAllForMemberAsync with memberId: {memberId} ended at {DateTime.UtcNow}");

        return socials;
    }
}