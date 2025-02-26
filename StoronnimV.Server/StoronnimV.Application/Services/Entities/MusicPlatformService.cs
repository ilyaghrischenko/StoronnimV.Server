using Microsoft.Extensions.Logging;
using StoronnimV.Application.Contracts.Entities;
using StoronnimV.Application.DTO.Requests.Entities.Pages.Addition;
using StoronnimV.Application.Exceptions;
using StoronnimV.Domain.Contracts;
using StoronnimV.Domain.Contracts.AzureBlobStorage;
using StoronnimV.Domain.Contracts.Database;
using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Projections;

namespace StoronnimV.Application.Services.Entities;

public class MusicPlatformService(
    IMusicPlatformRepository musicPlatformRepository,
    IBlobRepository blobRepository) : IMusicPlatformService
{
    private readonly IMusicPlatformRepository _musicPlatformRepository = musicPlatformRepository;
    private readonly IBlobRepository _blobRepository = blobRepository;
    
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

    /// <summary>
    /// Music platform addition to database
    /// </summary>
    /// <param name="request">MusicPlatformAdditionRequest</param>
    /// <param name="ct">CancellationToken</param>
    public async Task AddMusicPlatformAsync(MusicPlatformAdditionRequest request, CancellationToken ct)
    {
        MusicPlatform musicPlatform = new()
        {
            BgImageUrl = "default",
            PlatformUrl = request.PlatformUrl
        };
        
        await _musicPlatformRepository.AddAsync(musicPlatform, ct);
        
        string musicPlatformBlobName = $"music-platform-{musicPlatform.Id}";
        string musicPlatformPhotoUrl = await _blobRepository
            .AddFileAndGetUrlAsync("storonnimv-photo", musicPlatformBlobName, request.BgImageUrl.OpenReadStream(), ct);
        
        await _musicPlatformRepository.UpdateAsync(musicPlatform, () => musicPlatform.BgImageUrl = musicPlatformPhotoUrl, ct);
    }
    
    /// <summary>
    /// Music platform deletion from database
    /// </summary>
    /// <param name="id">long</param>
    /// <param name="ct">CancellationToken</param>
    /// <exception cref="EntityNotFoundException"></exception>
    public async Task DeleteMusicPlatformAsync(long id, CancellationToken ct)
    {
        MusicPlatform? musicPlatform = await _musicPlatformRepository.GetByIdAsync(id, ct);

        if (musicPlatform is null)
        {
            throw new EntityNotFoundException($"Music platform with {nameof(id)}: {id} was not found");
        }

        await _musicPlatformRepository.DeleteAsync(musicPlatform, ct);

        await _blobRepository.DeleteAllFilesByNameAsync("storonnimv-photo", $"music-platform-{id}", ct);
    }
}