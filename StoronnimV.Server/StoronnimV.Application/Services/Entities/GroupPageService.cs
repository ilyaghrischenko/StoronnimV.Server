using Microsoft.Extensions.Logging;
using StoronnimV.Application.Contracts.Entities;
using StoronnimV.Application.Contracts.Entities.Shared;
using StoronnimV.Application.Exceptions;
using StoronnimV.Domain.Contracts;
using StoronnimV.Domain.Contracts.Database;
using StoronnimV.Domain.Projections;

namespace StoronnimV.Application.Services.Entities;

/// <summary>
/// Сервис для проверки полученных данных, полученых с репозитория
/// </summary>
/// <param name="groupPageRepository"></param>
public class GroupPageService(IGroupPageRepository groupPageRepository) : IGroupPageService
{
    private readonly IGroupPageRepository _groupPageRepository = groupPageRepository;
    
    public async Task<GroupPageProjection> GetItemByIdAsync(long id, CancellationToken ct)
    {
        GroupPageProjection groupPage = await _groupPageRepository.GetByIdAsNoTrackingAsync(id, ct)
                                        ?? throw new EntityNotFoundException($"GroupPage with id: {id} was not found");
        
        return groupPage;
    }

    public async Task<IEnumerable<GroupPageProjection>> GetAllAsync(CancellationToken ct)
    {
        var groupPages = await _groupPageRepository.GetAllAsNoTrackingAsync(ct);
        
        return groupPages ?? new List<GroupPageProjection>();
    }
    
    public async Task<GroupPageProjection> GetFirstGroupPageAsync(CancellationToken ct)
    {
        GroupPageProjection groupPage = await _groupPageRepository.GetFirstGroupPageAsync(ct)
                                        ?? throw new EntityNotFoundException($"GroupPage was not found");
        
        return groupPage;
    }

}