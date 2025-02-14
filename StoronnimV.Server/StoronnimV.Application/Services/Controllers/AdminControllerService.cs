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
        _logger.LogInformation($"Service: AdminControllerService Method: DeleteNewsItemAsync started at {DateTime.UtcNow}");

        await _adminService.DeleteNewsItemAsync(id, ct);
        
        _logger.LogInformation($"Service: AdminControllerService Method: DeleteNewsItemAsync ended at {DateTime.UtcNow}");
    }
}