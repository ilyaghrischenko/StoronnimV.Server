using AutoMapper;
using Microsoft.Extensions.Logging;
using StoronnimV.Application.Interfaces.Controllers;
using StoronnimV.Application.Interfaces.Entities;

namespace StoronnimV.Application.Services.Controllers;

public class AccountControllerService(
    IAdminService adminService,
    ILogger<AccountControllerService> logger) : IAccountControllerService
{
    private readonly IAdminService _adminService = adminService;
    private readonly ILogger<AccountControllerService> _logger = logger;


    public async Task<string> LogInAsync(string login, string password)
    {
        _logger.LogInformation($"Service: AccountControllerService Method: LogInAsync with [login: {login}, password: {password}] started at {DateTime.UtcNow}");
        
        
        
        _logger.LogInformation($"Service: AccountControllerService Method: LogInAsync with [login: {login}, password: {password}] ended at {DateTime.UtcNow}");

        return ;
    }
}