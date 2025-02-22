using AutoMapper;
using StoronnimV.Application.Contracts.Controllers;
using StoronnimV.Application.Contracts.Entities;
using StoronnimV.Application.DTO.Requests.Entities.Admin;
using StoronnimV.Application.DTO.Responses.Admin;

namespace StoronnimV.Application.Services.Controllers;

public class SuperAdminControllerService(
    IAdminService adminService,
    IMapper mapper) : ISuperAdminControllerService
{
    private readonly IAdminService _adminService = adminService;
    private readonly IMapper _mapper = mapper;
    
    public async Task<IEnumerable<BasicAdminResponse>> GetAllAsync(CancellationToken ct)
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
}