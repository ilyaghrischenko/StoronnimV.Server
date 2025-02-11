using System.Configuration;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using StoronnimV.Application.AutentificationOptions;
using StoronnimV.Application.Contracts.BlobAzure;
using StoronnimV.Application.Contracts.Controllers;
using StoronnimV.Application.Contracts.Entities;
using StoronnimV.Application.Contracts.Home;
using StoronnimV.Application.Contracts.Jwt;
using StoronnimV.Application.Mapping.Group;
using StoronnimV.Application.Mapping.Home;
using StoronnimV.Application.Mapping.Music;
using StoronnimV.Application.Mapping.News;
using StoronnimV.Application.Mapping.Schedule;
using StoronnimV.Application.Mapping.Video;
using StoronnimV.Application.Services.BlobAzure;
using StoronnimV.Application.Services.Controllers;
using StoronnimV.Application.Services.Entities;
using StoronnimV.Application.Services.Hangfire;
using StoronnimV.Application.Services.Home;
using StoronnimV.Application.Services.Jwt;
using StoronnimV.Infrastructure;
using StoronnimV.Infrastructure.Repositories;
using StoronnimV.Infrastructure.Repositories.Shared;
using StoronnimV.Domain.Contracts;
using StoronnimV.Domain.Contracts.Shared;
using StoronnimV.Domain.Entities;

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
        builder.Services.AddScoped<IAdminService, AdminService>();
        
        builder.Services.AddScoped<INewsControllerService, NewsControllerService>();
        builder.Services.AddScoped<ISchedulesControllerService, SchedulesControllerService>();
        builder.Services.AddScoped<IGroupPageControllerService, GroupPageControllerService>();
        builder.Services.AddScoped<IMusicControllerService, MusicControllerService>();
        builder.Services.AddScoped<IVideoControllerService, VideoControllerService>();
        builder.Services.AddScoped<IHomeControllerService, HomeControllerService>();
        builder.Services.AddScoped<IAccountControllerService, AccountControllerService>();
        builder.Services.AddScoped<IAdminControllerService, AdminControllerService>();

        builder.Services.AddScoped<IHomeService, HomeService>();
        builder.Services.AddScoped<IJwtBearerService, JwtBearerService>();
        builder.Services.AddScoped<IBlobService, BlobService>();
        
        return builder;
    }

    public static WebApplicationBuilder AddIntegrationServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ScheduleStatusUpdaterService>();
        builder.Services.AddTransient<IPasswordHasher<Admin>, PasswordHasher<Admin>>();
        
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
        builder.Services.AddScoped<IAdminRepository, AdminRepository>();
        
        return builder;
    }

    public static WebApplicationBuilder AddPooledDbContextFactory(this WebApplicationBuilder builder)
    {
        string? connectionString = builder.Configuration.GetConnectionString("CloudConnection");
        
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
        string? connectionString = builder.Configuration.GetConnectionString("CloudConnection");
        
        builder.Services.AddHangfire(config => config
            .UsePostgreSqlStorage(connectionString));

        builder.Services.AddHangfireServer();
        
        return builder;
    }

    public static WebApplicationBuilder AddJwtBearer(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidIssuer = JwtOptions.ISSUER,

                    ValidateAudience = true,
                    ValidAudience = JwtOptions.AUDIENCE,

                    ValidateLifetime = true,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = JwtOptions.GetKey()
                };
            });
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                Description =
                    "Input your JWT token in the 'Authorization' header like this: \"Authorization: Bearer {yourJWT}\""
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        return builder;
    }
}