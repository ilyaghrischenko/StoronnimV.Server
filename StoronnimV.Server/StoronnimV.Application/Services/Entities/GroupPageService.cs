using Microsoft.Extensions.Logging;
using StoronnimV.Application.Contracts.Entities;
using StoronnimV.Application.Contracts.Entities.Shared;
using StoronnimV.Application.DTO.Requests.Entities.Pages.Addition;
using StoronnimV.Application.Exceptions;
using StoronnimV.Domain.Contracts;
using StoronnimV.Domain.Contracts.AzureBlobStorage;
using StoronnimV.Domain.Contracts.Database;
using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Projections;

namespace StoronnimV.Application.Services.Entities;

/// <summary>
/// Сервис для проверки полученных данных, полученых с репозитория
/// </summary>
/// <param name="groupPageRepository"></param>
public class GroupPageService(
    IGroupPageRepository groupPageRepository,
    IBlobRepository blobRepository) : IGroupPageService
{
    public async Task<GroupPageProjection> GetItemByIdAsync(long id, CancellationToken ct)
    {
        GroupPageProjection groupPage = await groupPageRepository.GetByIdAsNoTrackingAsync(id, ct)
                                        ?? throw new EntityNotFoundException($"GroupPage with {nameof(id)}: {id} was not found");
        
        return groupPage;
    }

    public async Task<IEnumerable<GroupPageProjection>> GetAllAsync(CancellationToken ct)
    {
        var groupPages = await groupPageRepository.GetAllAsNoTrackingAsync(ct);
        
        return groupPages ?? new List<GroupPageProjection>();
    }
    
    public async Task<GroupPageProjection> GetFirstGroupPageAsync(CancellationToken ct)
    {
        GroupPageProjection groupPage = await groupPageRepository.GetFirstGroupPageAsync(ct)
                                        ?? throw new EntityNotFoundException($"GroupPage was not found");
        
        return groupPage;
    }

    /// <summary>
    /// GroupPage addition to database
    /// </summary>
    /// <param name="request">GroupPageAdditionRequest</param>
    /// <param name="ct">CancellationToken</param>
    public async Task AddGroupPageAsync(GroupPageAdditionRequest request, CancellationToken ct)
    {
        GroupPage groupPage = new()
        {
            PhotoUrl = "default",
            Description = request.Description,
        };
        
        await groupPageRepository.AddAsync(groupPage, ct);
        
        string groupPageBlobName = $"group-page-{groupPage.Id}";
        string groupPagePhotoUrl = await blobRepository.AddFileAndGetUrlAsync("storonnimv-photo", groupPageBlobName, request.PhotoUrl.OpenReadStream(), ct);
        
        await groupPageRepository.UpdateAsync(groupPage, () => groupPage.PhotoUrl = groupPagePhotoUrl, ct);
    }

    /// <summary>
    /// GroupPage deletion from database
    /// </summary>
    /// <param name="id">long</param>
    /// <param name="ct">CancellationToken</param>
    /// <exception cref="EntityNotFoundException">EntityNotFoundException</exception>
    public async Task DeleteGroupPageAsync(long id, CancellationToken ct)
    {
        GroupPage? groupPage = await groupPageRepository.GetByIdAsync(id, ct);

        if (groupPage is null)
        {
            throw new EntityNotFoundException($"Group page with {nameof(id)}: {id} was not found");
        }

        await groupPageRepository.DeleteAsync(groupPage, ct);

        await blobRepository.DeleteAllFilesByNameAsync("storonnimv-photo", $"group-page-{id}", ct);
    }
    
    //todo: update group page
    // public async Task UpdateGroupPageAsync(GroupPageUpdateRequest request, CancellationToken ct)
}