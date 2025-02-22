using Microsoft.AspNetCore.Identity;
using StoronnimV.Application.Contracts.Identity;
using StoronnimV.Application.Exceptions;
using StoronnimV.Domain.Contracts.Database;
using StoronnimV.Domain.Entities;

namespace StoronnimV.Application.Services.Identity;

//TODO: придумать, по возможности, как сделать сервис универсальным (не подвязанным чисто к админу) GENERIC
public class AccountService(
    IAdminRepository adminRepository,
    IPasswordHasher<Admin> passwordHasher) : IAccountService
{
    private readonly IAdminRepository _adminRepository = adminRepository;
    private readonly IPasswordHasher<Admin> _passwordHasher = passwordHasher;
    
    public async Task<Admin> LogInAsync(string login, string password, CancellationToken ct)
    {
        Admin? admin = await _adminRepository.GetByLoginAsync(login, ct);

        if (admin is null)
        {
            throw new LogInException($"Admin with {nameof(login)}: {login} was not found");
        }
        
        PasswordVerificationResult verificationResult = _passwordHasher.VerifyHashedPassword(admin, admin.Password, password);

        if (verificationResult == PasswordVerificationResult.Failed)
        {
            throw new LogInException("Wrong password");
        }
        
        return admin;
    }
}