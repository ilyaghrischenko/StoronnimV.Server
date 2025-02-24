using Microsoft.Extensions.Logging;
using StoronnimV.Application.Contracts.Entities;
using StoronnimV.Application.Exceptions;
using StoronnimV.Domain.Contracts;
using StoronnimV.Domain.Contracts.Database;
using StoronnimV.Domain.Projections;

namespace StoronnimV.Application.Services.Entities;

public class MusicPlatformService(IMusicPlatformRepository musicPlatformRepository) : IMusicPlatformService
{
    private readonly IMusicPlatformRepository _musicPlatformRepository = musicPlatformRepository;

    public async Task<MusicPlatformProjection> GetItemByIdAsync(long id, CancellationToken ct)
    {
        MusicPlatformProjection musicPlatform = await _musicPlatformRepository.GetByIdAsNoTrackingAsync(id, ct)
                                                ?? throw new EntityNotFoundException($"Music Platform with {nameof(id)}: {id} was not found");
        
        return musicPlatform;
    }

    public async Task<IEnumerable<MusicPlatformProjection>> GetAllAsync(CancellationToken ct)
    {
        var allMusicPlatforms = await _musicPlatformRepository.GetAllAsNoTrackingAsync(ct);
        if (allMusicPlatforms is null || !allMusicPlatforms.Any())
        {
            return new List<MusicPlatformProjection>();
        }
        
        return allMusicPlatforms
            .ToList();
    }
}