using Microsoft.Extensions.Logging;
using StoronnimV.Application.Contracts.Entities;
using StoronnimV.Application.Exceptions;
using StoronnimV.Domain.Contracts;
using StoronnimV.Domain.Projections;

namespace StoronnimV.Application.Services.Entities;

/// <summary>
/// Сервис для проверки полученных данных, полученых с репозитория
/// </summary>
/// <param name="groupPageRepository"></param>
public class GroupPageService(IGroupPageRepository groupPageRepository,
    ILogger<GroupPageService> logger) : IGroupPageService
{
    private readonly IGroupPageRepository _groupPageRepository = groupPageRepository;
    private readonly ILogger<GroupPageService> _logger = logger;
    
    public async Task<GroupPageProjection> GetItemByIdAsync(long id, CancellationToken ct)
    {
        _logger.LogInformation($"Service: GroupPageService Method: GetItemByIdAsync with id: {id} started at {DateTime.UtcNow}");
        
        GroupPageProjection groupPage = await _groupPageRepository.GetByIdAsNoTrackingAsync(id, ct)
                                        ?? throw new EntityNotFoundException($"GroupPage with id: {id} was not found");
        
        _logger.LogInformation($"Service: GroupPageService Method: GetItemByIdAsync with id: {id} ended at {DateTime.UtcNow}");

        return groupPage;
    }

    public async Task<IEnumerable<GroupPageProjection>> GetAllAsync(CancellationToken ct)
    {
        _logger.LogInformation($"Service: GroupPageService Method: GetAllAsync started at {DateTime.UtcNow}");
        
        var groupPages = await _groupPageRepository.GetAllAsNoTrackingAsync(ct);
        
        _logger.LogInformation($"Service: GroupPageService Method: GetAllAsync ended at {DateTime.UtcNow}");

        return groupPages ?? new List<GroupPageProjection>();
    }
    
    public async Task<GroupPageProjection> GetFirstGroupPageAsync(CancellationToken ct)
    {
        _logger.LogInformation($"Service: GroupPageService Method: GetFirstGroupPageAsync started at {DateTime.UtcNow}");
        
        GroupPageProjection groupPage = await _groupPageRepository.GetFirstGroupPageAsync(ct)
                                        ?? throw new EntityNotFoundException($"GroupPage was not found");
        
        _logger.LogInformation($"Service: GroupPageService Method: GetFirstGroupPageAsync ended at {DateTime.UtcNow}");

        return groupPage;
    }
}