using AutoMapper;
using StoronnimV.Application.Contracts.Controllers;
using StoronnimV.Application.Contracts.Entities;
using StoronnimV.Application.DTO.Requests.Entities.Admin;
using StoronnimV.Application.DTO.Responses.Admin;

namespace StoronnimV.Application.Services.Controllers;

public class SuperAdminControllerService(
    ISuperAdminService superAdminService,
    IMapper mapper) : ISuperAdminControllerService
{
    private readonly ISuperAdminService _superAdminService = superAdminService;
    private readonly IMapper _mapper = mapper;
    
    public async Task<IEnumerable<BasicAdminResponse>> GetAllAsync(CancellationToken ct)
    {
        var basicAdminProjections = await _superAdminService.GetAllAsync(ct);

        var basicAdminDtos = _mapper.Map<IEnumerable<BasicAdminResponse>>(basicAdminProjections);
        return basicAdminDtos;
    }

    public async Task DeleteBasicAdminAsync(long id, CancellationToken ct)
    {
        await _superAdminService.DeleteBasicAdminAsync(id, ct);
    }

    public async Task AddBasicAdminAsync(CreateBasicAdminRequest request, CancellationToken ct)
    {
        string login = request.Login;
        string unhashedPassword = request.Password;
        
        await _superAdminService.AddBasicAdminAsync(login, unhashedPassword, ct);
    }

    public async Task EditBasicAdminPasswordAsync(long id, EditBasicAdminPasswordRequest passwordRequest, CancellationToken ct)
    {
        string oldPassword = passwordRequest.OldPassword;
        string newPassword = passwordRequest.NewPassword;
        
        await _superAdminService.EditBasicAdminPasswordAsync(id, oldPassword, newPassword, ct);
    }

    public async Task EditBasicAdminLoginAsync(long id, EditBasicAdminLoginRequest loginRequest, CancellationToken ct)
    {
        string newLogin = loginRequest.NewLogin;
        
        await _superAdminService.EditBasicAdminLoginAsync(id, newLogin, ct);
    }
}