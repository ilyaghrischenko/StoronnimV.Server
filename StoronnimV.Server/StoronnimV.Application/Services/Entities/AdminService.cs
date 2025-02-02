using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using StoronnimV.Application.DTO.Requests.Account;
using StoronnimV.Application.Exceptions;
using StoronnimV.Application.Interfaces.Entities;
using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Interfaces;
using StoronnimV.Domain.Projections;

namespace StoronnimV.Application.Services.Entities;

public class AdminService(
    IAdminRepository adminRepository,
    ILogger<AdminService> logger,
    IPasswordHasher<Admin> passwordHasher) : IAdminService
{
    private readonly IAdminRepository _adminRepository = adminRepository;
    private readonly ILogger<AdminService> _logger = logger;
    private readonly IPasswordHasher<Admin> _passwordHasher = passwordHasher;
    
    public async Task<AdminProjection> GetItemByIdAsync(long id, CancellationToken ct)
    {
        _logger.LogInformation($"Service: AdminService Method: GetItemByIdAsync with id: {id} started at {DateTime.UtcNow}");

        AdminProjection? admin = await _adminRepository.GetByIdAsNoTrackingAsync(id, ct);

        if (admin is null)
        {
            throw new EntityNotFoundException($"Admin with id: {id} was not found");
        }
        
        _logger.LogInformation($"Service: AdminService Method: GetItemByIdAsync with id: {id} ended at {DateTime.UtcNow}");

        return admin;
    }

    public async Task<IEnumerable<AdminProjection>> GetAllAsync(CancellationToken ct)
    {
        _logger.LogInformation($"Service: AdminService Method: GetAllAsync started at {DateTime.UtcNow}");
        
        var admins = await _adminRepository.GetAllAsNoTrackingAsync(ct);

        if (admins is null || !admins.Any())
        {
            return new List<AdminProjection>();
        }
        
        _logger.LogInformation($"Service: AdminService Method: GetAllAsync ended at {DateTime.UtcNow}");

        return admins;
    }

    public async Task<Admin> LogInAsync(LogInRequest request, CancellationToken ct)
    {
        _logger.LogInformation($"Service: AdminService Method: LogInAsync started at {DateTime.UtcNow}");
        
        Admin? admin = await _adminRepository.GetByLoginAsync(request.Login, ct);

        if (admin is null)
        {
            throw new LogInException($"Admin with login: {request.Login} was not found");
        }
        
        PasswordVerificationResult verificationResult = _passwordHasher.VerifyHashedPassword(admin, admin.Password, request.Password);

        if (verificationResult == PasswordVerificationResult.Failed)
        {
            throw new LogInException("Wrong password");
        }
        
        _logger.LogInformation($"Service: AdminService Method: LogInAsync started at {DateTime.UtcNow}");

        return admin;
    }
}