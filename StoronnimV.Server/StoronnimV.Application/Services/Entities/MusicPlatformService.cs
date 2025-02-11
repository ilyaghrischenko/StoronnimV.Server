using Microsoft.Extensions.Logging;
using StoronnimV.Application.Contracts.Entities;
using StoronnimV.Application.Exceptions;
using StoronnimV.Domain.Contracts;
using StoronnimV.Domain.Contracts.Database;
using StoronnimV.Domain.Projections;

namespace StoronnimV.Application.Services.Entities;

public class MusicPlatformService(IMusicPlatformRepository musicPlatformRepository,
    ILogger<MusicPlatformService> logger) : IMusicPlatformService
{
    private readonly IMusicPlatformRepository _musicPlatformRepository = musicPlatformRepository;
    private readonly ILogger<MusicPlatformService> _logger = logger;

    public async Task<MusicPlatformProjection> GetItemByIdAsync(long id, CancellationToken ct)
    {
        _logger.LogInformation($"Service: MusicPlatformService Method: GetItemByIdAsync with id: {id} started at {DateTime.UtcNow}");
        
        MusicPlatformProjection musicPlatform = await _musicPlatformRepository.GetByIdAsNoTrackingAsync(id, ct)
                                                ?? throw new EntityNotFoundException($"Music Platform with id: {id} was not found");
        
        _logger.LogInformation($"Service: MusicPlatformService Method: GetItemByIdAsync with id: {id} ended at {DateTime.UtcNow}");
        
        return musicPlatform;
    }

    public async Task<IEnumerable<MusicPlatformProjection>> GetAllAsync(CancellationToken ct)
    {
        _logger.LogInformation($"Service: MusicPlatformService Method: GetAllAsync started at {DateTime.UtcNow}");
        
        var allMusicPlatforms = await _musicPlatformRepository.GetAllAsNoTrackingAsync(ct);
        if (allMusicPlatforms is null || !allMusicPlatforms.Any())
        {
            return new List<MusicPlatformProjection>();
        }
        
        _logger.LogInformation($"Service: MusicPlatformService Method: GetAllAsync ended at {DateTime.UtcNow}");
        
        return allMusicPlatforms
            .ToList();
    }
}