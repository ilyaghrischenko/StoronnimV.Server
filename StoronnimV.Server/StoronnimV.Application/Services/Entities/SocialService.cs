using Microsoft.Extensions.Logging;
using StoronnimV.Application.Contracts.Entities;
using StoronnimV.Application.Exceptions;
using StoronnimV.Domain.Contracts;
using StoronnimV.Domain.Contracts.Database;
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
        SocialProjection social = await _socialRepository.GetByIdAsNoTrackingAsync(id, ct)
                                       ?? throw new EntityNotFoundException($"Social with id: {id} was not found");
        
        return social;
    }
}