using AutoMapper;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using StoronnimV.Api.Extensions;
using StoronnimV.Api.Middlewares;
using StoronnimV.Application.Mapping.Admin;
using StoronnimV.Application.Mapping.Group;
using StoronnimV.Application.Mapping.Home;
using StoronnimV.Application.Mapping.News;
using StoronnimV.Application.Mapping.Schedule;
using StoronnimV.Application.Services.Hangfire;
using StoronnimV.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder
    .AddSerilogLogger()
    .AddAutoMapper()
    .AddCors()
    .AddRepositories()
    .AddApplicationServices()
    .AddIntegrationServices()
    .AddHangfire()
    .AddPooledDbContextFactory()
    .AddJwtBearer();
    
#region AutoMapper
MapperConfiguration mapperConfig = new(cfg =>
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
    
    #region Home
    cfg.AddProfile<HomeNewsMappingProfile>();
    cfg.AddProfile<HomeScheduleMappingProfile>();
    #endregion
    
    #region Admin
    cfg.AddProfile<BasicAdminMappingProfile>();
    #endregion
});

mapperConfig.AssertConfigurationIsValid();
#endregion

// Add services to the container.
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

WebApplication app = builder.Build();

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
app.UseAuthorization();
app.UseStaticFiles();

app.UseCors("AllowReactApp");

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<LoggingMiddleware>();

app.UseHangfireDashboard();
app.MapHangfireDashboard();

#region DatabaseInitializer
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var dbContextFactory = services.GetRequiredService<IDbContextFactory<StoronnimVContext>>();
        await using StoronnimVContext context = dbContextFactory.CreateDbContext();
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
    service => service.UpdateScheduleStatusesAsync(CancellationToken.None),
    Cron.Daily);
#endregion

app.Run();