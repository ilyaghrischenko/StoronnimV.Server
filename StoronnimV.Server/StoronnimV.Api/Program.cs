using AutoMapper;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.EntityFrameworkCore;
using Serilog;
using StoronnimV.Api.Extensions;
using StoronnimV.Api.Middlewares;
using StoronnimV.Application.Interfaces.Controllers;
using StoronnimV.Application.Interfaces.Entities;
using StoronnimV.Application.Mapping.Group;
using StoronnimV.Application.Mapping.Music;
using StoronnimV.Application.Mapping.News;
using StoronnimV.Application.Mapping.Schedule;
using StoronnimV.Application.Mapping.Video;
using StoronnimV.Application.Services.Controllers;
using StoronnimV.Application.Services.Entities;
using StoronnimV.Application.Services.Hangfire;
using StoronnimV.Data;
using StoronnimV.Data.Repositories;
using StoronnimV.Data.Repositories.Shared;
using StoronnimV.Domain.Enums;
using StoronnimV.Domain.Interfaces;
using StoronnimV.Domain.Interfaces.Shared;

var builder = WebApplication.CreateBuilder(args);

builder
    .AddSerilogLogger()
    .AddAutoMapper()
    .AddCors()
    .AddRepositories()
    .AddApplicationServices()
    .AddIntegrationServices()
    .AddHangfire()
    .AddPooledDbContextFactory();
    
#region AutoMapper
var mapperConfig = new MapperConfiguration(cfg =>
{
    #region Group
    cfg.AddProfile<GroupPageMappingProfile>();
    cfg.AddProfile<MemberShortMappingProfile>();
    cfg.AddProfile<MemberMappingProfile>();
    cfg.AddProfile<SocialMappingProfile>();
    #endregion
    
    #region News
    cfg.AddProfile<NewsMappingProfile>();
    cfg.AddProfile<NewsShortMappingProfile>();
    #endregion
    
    #region Schedule
    cfg.AddProfile<ScheduleMappingProfile>();
    cfg.AddProfile<ScheduleShortMappingProfile>();
    #endregion
    
});

mapperConfig.AssertConfigurationIsValid();
#endregion

// Add services to the container.
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();
app.UseRouting();
app.UseStaticFiles();

app.UseCors("AllowReactApp");

app.UseMiddleware<ExceptionMiddleware>();

app.UseHangfireDashboard();
app.MapHangfireDashboard();

#region DatabaseInitializer
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var dbContextFactory = services.GetRequiredService<IDbContextFactory<StoronnimVContext>>();
        using var context = dbContextFactory.CreateDbContext();
        DatabaseInitializer.Initialize(context);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred while initializing the database: {ex.Message}");
    }
}
#endregion

#region StatusUpdaterSettings
RecurringJob.AddOrUpdate<ScheduleStatusUpdaterService>(
    "update-schedule-statuses",
    service => service.UpdateScheduleStatusesAsync(),
    Cron.Daily);
#endregion

app.Run();