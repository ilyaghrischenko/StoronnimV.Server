using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.EntityFrameworkCore;
using Serilog;
using StoronnimV.Application.Interfaces.Controllers;
using StoronnimV.Application.Interfaces.Entities;
using StoronnimV.Application.Interfaces.Home;
using StoronnimV.Application.Mapping.Group;
using StoronnimV.Application.Mapping.Home;
using StoronnimV.Application.Mapping.Music;
using StoronnimV.Application.Mapping.News;
using StoronnimV.Application.Mapping.Schedule;
using StoronnimV.Application.Mapping.Video;
using StoronnimV.Application.Services.Controllers;
using StoronnimV.Application.Services.Entities;
using StoronnimV.Application.Services.Hangfire;
using StoronnimV.Application.Services.Home;
using StoronnimV.Data;
using StoronnimV.Data.Repositories;
using StoronnimV.Data.Repositories.Shared;
using StoronnimV.Domain.Interfaces;
using StoronnimV.Domain.Interfaces.Shared;

namespace StoronnimV.Api.Extensions;

public static class ServiceCollectionsExtension
{
    public static WebApplicationBuilder AddApplicationServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<INewsService, NewsService>();
        builder.Services.AddScoped<IScheduleService, ScheduleService>();
        builder.Services.AddScoped<ISocialService, SocialService>();
        builder.Services.AddScoped<IMemberService, MemberService>();
        builder.Services.AddScoped<IGroupPageService, GroupPageService>();
        builder.Services.AddScoped<IMusicPlatformService, MusicPlatformService>();
        builder.Services.AddScoped<IVideoService, VideoService>();
        
        builder.Services.AddScoped<INewsControllerService, NewsControllerService>();
        builder.Services.AddScoped<ISchedulesControllerService, SchedulesControllerService>();
        builder.Services.AddScoped<IGroupPageControllerService, GroupPageControllerService>();
        builder.Services.AddScoped<IMusicControllerService, MusicControllerService>();
        builder.Services.AddScoped<IVideoControllerService, VideoControllerService>();
        builder.Services.AddScoped<IHomeControllerService, HomeControllerService>();

        builder.Services.AddScoped<IHomeService, HomeService>();
        
        return builder;
    }

    public static WebApplicationBuilder AddIntegrationServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ScheduleStatusUpdaterService>();
        
        return builder;
    }

    public static WebApplicationBuilder AddRepositories(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        builder.Services.AddScoped<INewsRepository, NewsRepository>();
        builder.Services.AddScoped<ISocialRepository, SocialRepository>();
        builder.Services.AddScoped<IMemberRepository, MemberRepository>();
        builder.Services.AddScoped<IGroupPageRepository, GroupPageRepository>();
        builder.Services.AddScoped<IScheduleRepository, ScheduleRepository>();
        builder.Services.AddScoped<IMusicPlatformRepository, MusicPlatformRepository>();
        builder.Services.AddScoped<IVideoRepository, VideoRepository>();
        
        return builder;
    }

    public static WebApplicationBuilder AddPooledDbContextFactory(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("CloudConnection");
        
        builder.Services.AddPooledDbContextFactory<StoronnimVContext>(options =>
            options.UseNpgsql(connectionString));
        
        return builder;
    }

    public static WebApplicationBuilder AddSerilogLogger(this WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("../logs/log.txt",
                rollingInterval: RollingInterval.Day,
                restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error)
            .CreateLogger();

        builder.Host.UseSerilog();
        builder.Services.AddLogging();
        
        return builder;
    }

    public static WebApplicationBuilder AddAutoMapper(this WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(typeof(NewsMappingProfile).Assembly);
        builder.Services.AddAutoMapper(typeof(NewsShortMappingProfile).Assembly);
        
        builder.Services.AddAutoMapper(typeof(ScheduleMappingProfile).Assembly);
        builder.Services.AddAutoMapper(typeof(ScheduleShortMappingProfile).Assembly);
        
        builder.Services.AddAutoMapper(typeof(GroupPageMappingProfile).Assembly);
        builder.Services.AddAutoMapper(typeof(MemberShortMappingProfile).Assembly);
        builder.Services.AddAutoMapper(typeof(MemberMappingProfile).Assembly);
        builder.Services.AddAutoMapper(typeof(SocialMappingProfile).Assembly);

        builder.Services.AddAutoMapper(typeof(HomeNewsMappingProfile).Assembly);
        builder.Services.AddAutoMapper(typeof(HomeScheduleMappingProfile).Assembly);
        
        return builder;
    }

    public static WebApplicationBuilder AddCors(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowReactApp",
                policy =>
                {
                    policy.WithOrigins("http://localhost:5173")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });
        
        return builder;
    }

    public static WebApplicationBuilder AddHangfire(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("CloudConnection");
        
        builder.Services.AddHangfire(config => config
            .UsePostgreSqlStorage(connectionString));

        builder.Services.AddHangfireServer();
        
        return builder;
    }
}