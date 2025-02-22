using Microsoft.AspNetCore.Identity;
using StoronnimV.Application.Contracts.Entities;
using StoronnimV.Application.Exceptions;
using StoronnimV.Domain.Contracts.Database;
using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Projections.Admin;

namespace StoronnimV.Application.Services.Entities;

public class SuperAdminService(
    IAdminRepository adminRepository,
    IPasswordHasher<Admin> passwordHasher) : ISuperAdminService
{
    private readonly IAdminRepository _adminRepository = adminRepository;
    private readonly IPasswordHasher<Admin> _passwordHasher = passwordHasher;
    
    public async Task<IEnumerable<BasicAdminProjection>> GetAllAsync(CancellationToken ct)
    {
        var basicAdmins = await _adminRepository.GetAllBasicAdminsAsync(ct);

        return basicAdmins ?? new List<BasicAdminProjection>();
    }

    public async Task DeleteBasicAdminAsync(long id, CancellationToken ct)
    {
        Admin? basicAdmin = await _adminRepository.GetByIdAsync(id, ct);

        if (basicAdmin is null)
        {
            throw new EntityNotFoundException($"Basic Admin with {nameof(id)}: {id} was not found");
        }
        
        await _adminRepository.DeleteAsync(basicAdmin, ct);
    }

    public async Task AddBasicAdminAsync(string login, string unhashedPassword, CancellationToken ct)
    {
        var allBasicAdmins = (await _adminRepository.GetAllBasicAdminsAsync(ct))
            ?.ToList();

        ThrowExceptionIfLoginAlreadyExists(login, allBasicAdmins);
        
        string hashedPassword = _passwordHasher.HashPassword(null! ,unhashedPassword);

        Admin newBasicAdmin = new()
        {
            Login = login,
            Password = hashedPassword
        };
        
        await _adminRepository.AddAsync(newBasicAdmin, ct);
    }

    public async Task EditBasicAdminLoginAsync(long id, string newLogin, CancellationToken ct)
    {
        Admin? adminToChange = await _adminRepository.GetByIdAsync(id, ct);
        
        if (adminToChange is null)
        {
            throw new EntityNotFoundException($"Admin with {nameof(id)}: {id} was not found");
        }

        var allBasicAdmins = (await _adminRepository.GetAllBasicAdminsAsync(ct))
            ?.ToList();

        ThrowExceptionIfLoginAlreadyExists(newLogin, allBasicAdmins);

        await _adminRepository.UpdateAsync(adminToChange, () =>
        {
            adminToChange.Login = newLogin;
        }, ct);
    }

    private void ThrowExceptionIfLoginAlreadyExists(string login, List<BasicAdminProjection>? basicAdmins)
    {
        if (basicAdmins?.Count == 0) return;
        
        BasicAdminProjection? adminWithTheSameLogin = basicAdmins?.FirstOrDefault(x => x.Login == login);
        if (adminWithTheSameLogin != null)
        {
            throw new ArgumentException($"Admin with {nameof(login)}: {login} already exists");
        }
    }

    public async Task EditBasicAdminPasswordAsync(long id, string oldPassword, string newUnhashedPassword, CancellationToken ct)
    {
        Admin? adminToChange = await _adminRepository.GetByIdAsync(id, ct);
        
        if (adminToChange is null)
        {
            throw new EntityNotFoundException($"Admin with {nameof(id)}: {id} was not found");
        }
        
        PasswordVerificationResult verificationResult = _passwordHasher.VerifyHashedPassword(adminToChange, adminToChange.Password, oldPassword);

        if (verificationResult == PasswordVerificationResult.Failed)
        {
            throw new ArgumentException("passwords do not match");
        }
        
        string newHashedPassword = _passwordHasher.HashPassword(null!, newUnhashedPassword);

        await _adminRepository.UpdateAsync(adminToChange, () =>
        {
            adminToChange.Password = newHashedPassword;
        }, ct);
    }
}