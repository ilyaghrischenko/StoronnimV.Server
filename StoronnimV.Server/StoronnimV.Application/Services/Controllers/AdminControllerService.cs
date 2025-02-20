using AutoMapper;
using Microsoft.Extensions.Logging;
using StoronnimV.Application.Contracts.Controllers;
using StoronnimV.Application.Contracts.Entities;
using StoronnimV.Application.DTO.Responses.GroupPage;
using StoronnimV.Application.DTO.Responses.GroupPage.ShortGroupPage;
using StoronnimV.Application.DTO.Responses.GroupPage.ShortMember;
using StoronnimV.Application.DTO.Responses.NewsPage;
using StoronnimV.Application.DTO.Responses.Shared;
using StoronnimV.Application.DTO.Responses.Video;
using StoronnimV.Application.Models;
using StoronnimV.Domain.Projections;
using StoronnimV.Domain.Projections.Member;
using StoronnimV.Domain.Projections.News;
using StoronnimV.Domain.Projections.Video;

namespace StoronnimV.Application.Services.Controllers;

public class AdminControllerService(
    IAdminService adminService,
    ILogger<AdminControllerService> logger) : IAdminControllerService
{
    private readonly IAdminService _adminService = adminService;
    private readonly ILogger<AdminControllerService> _logger = logger;

    public async Task DeleteNewsItemAsync(long id, CancellationToken ct)
    {
        _logger.LogInformation($"Service: AdminControllerService Method: DeleteNewsItemAsync with id: {id} started at {DateTime.UtcNow}");

        await _adminService.DeleteNewsItemAsync(id, ct);
        
        _logger.LogInformation($"Service: AdminControllerService Method: DeleteNewsItemAsync with id: {id} ended at {DateTime.UtcNow}");
    }

    public async Task DeleteScheduleAsync(long id, CancellationToken ct)
    {
        _logger.LogInformation($"Service: AdminControllerService Method: DeleteScheduleAsync with id: {id} started at {DateTime.UtcNow}");
        
        await _adminService.DeleteScheduleAsync(id, ct);
        
        _logger.LogInformation($"Service: AdminControllerService Method: DeleteScheduleAsync with id: {id} ended at {DateTime.UtcNow}");
    }

    public async Task DeleteVideoAsync(long id, CancellationToken ct)
    {
        _logger.LogInformation($"Service: AdminControllerService Method: DeleteVideoAsync with id: {id} started at {DateTime.UtcNow}");

        await _adminService.DeleteVideoAsync(id, ct);
        
        _logger.LogInformation($"Service: AdminControllerService Method: DeleteVideoAsync with id: {id} ended at {DateTime.UtcNow}");
    }

    public async Task DeleteGroupPageAsync(long id, CancellationToken ct)
    {
        _logger.LogInformation($"Service: AdminControllerService Method: DeleteGroupPageAsync with id: {id} started at {DateTime.UtcNow}");
        
        await _adminService.DeleteGroupPageAsync(id, ct);
        
        _logger.LogInformation($"Service: AdminControllerService Method: DeleteGroupPageAsync with id: {id} ended at {DateTime.UtcNow}");
    }

    public async Task DeleteMemberAsync(long id, CancellationToken ct)
    {
        _logger.LogInformation($"Service: AdminControllerService Method: DeleteMemberAsync with id: {id} started at {DateTime.UtcNow}");
        
        await _adminService.DeleteMemberAsync(id, ct);
        
        _logger.LogInformation($"Service: AdminControllerService Method: DeleteMemberAsync with id: {id} ended at {DateTime.UtcNow}");
    }

    public async Task DeleteMusicPlatformAsync(long id, CancellationToken ct)
    {
        _logger.LogInformation($"Service: AdminControllerService Method: DeleteMusicPlatformAsync with id: {id} started at {DateTime.UtcNow}");
        
        await _adminService.DeleteMusicPlatformAsync(id, ct);
        
        _logger.LogInformation($"Service: AdminControllerService Method: DeleteMusicPlatformAsync with id: {id} ended at {DateTime.UtcNow}");
    }

    public async Task DeleteSocialAsync(long id, CancellationToken ct)
    {
        _logger.LogInformation($"Service: AdminControllerService Method: DeleteSocialAsync with id: {id} started at {DateTime.UtcNow}");
        
        await _adminService.DeleteSocialAsync(id, ct);
        
        _logger.LogInformation($"Service: AdminControllerService Method: DeleteSocialAsync with id: {id} ended at {DateTime.UtcNow}");
    }
}