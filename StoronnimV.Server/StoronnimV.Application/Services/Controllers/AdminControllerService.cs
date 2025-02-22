using AutoMapper;
using Microsoft.Extensions.Logging;
using StoronnimV.Application.Contracts.Controllers;
using StoronnimV.Application.Contracts.Entities;
using StoronnimV.Application.DTO.Requests.Entities.Admin;
using StoronnimV.Application.DTO.Responses.Admin;
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
    IMapper mapper,
    ILogger<AdminControllerService> logger) : IAdminControllerService
{
    private readonly IAdminService _adminService = adminService;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<AdminControllerService> _logger = logger;

    public async Task<IEnumerable<BasicAdminResponse>> GetAllBasicAdminsAsync(CancellationToken ct)
    {
        var basicAdminProjections = await _adminService.GetAllBasicAdminsAsync(ct);

        var basicAdminDtos = _mapper.Map<IEnumerable<BasicAdminResponse>>(basicAdminProjections);
        return basicAdminDtos;
    }

    public async Task DeleteBasicAdminAsync(long id, CancellationToken ct)
    {
        await _adminService.DeleteBasicAdminAsync(id, ct);
    }

    public async Task AddBasicAdminAsync(CreateBasicAdminRequest request, CancellationToken ct)
    {
        string login = request.Login;
        string unhashedPassword = request.Password;
        
        await _adminService.AddBasicAdminAsync(login, unhashedPassword, ct);
    }

    public async Task EditBasicAdminPasswordAsync(EditBasicAdminPasswordRequest passwordRequest, CancellationToken ct)
    {
        long id = passwordRequest.Id;
        string oldPassword = passwordRequest.OldPassword;
        string newPassword = passwordRequest.NewPassword;
        
        await _adminService.EditBasicAdminPasswordAsync(id, oldPassword, newPassword, ct);
    }

    public async Task EditBasicAdminLoginAsync(EditBasicAdminLoginRequest loginRequest, CancellationToken ct)
    {
        long id = loginRequest.Id;
        string newLogin = loginRequest.NewLogin;
        
        await _adminService.EditBasicAdminLoginAsync(id, newLogin, ct);
    }

    public async Task DeleteNewsItemAsync(long id, CancellationToken ct)
    {
        await _adminService.DeleteNewsItemAsync(id, ct);
    }

    public async Task DeleteScheduleAsync(long id, CancellationToken ct)
    {
        await _adminService.DeleteScheduleAsync(id, ct);
    }

    public async Task DeleteVideoAsync(long id, CancellationToken ct)
    {
        await _adminService.DeleteVideoAsync(id, ct);
    }

    public async Task DeleteGroupPageAsync(long id, CancellationToken ct)
    {
        await _adminService.DeleteGroupPageAsync(id, ct);
    }

    public async Task DeleteMemberAsync(long id, CancellationToken ct)
    {
        await _adminService.DeleteMemberAsync(id, ct);
    }

    public async Task DeleteMusicPlatformAsync(long id, CancellationToken ct)
    {
        await _adminService.DeleteMusicPlatformAsync(id, ct);
    }

    public async Task DeleteSocialAsync(long id, CancellationToken ct)
    {
        await _adminService.DeleteSocialAsync(id, ct);
    }
}