using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace StoronnimV.Infrastructure;

/// <summary>
/// Класс, который используется для создания объекта контекста для разработки.
/// </summary>
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<StoronnimVContext>
{
    public StoronnimVContext CreateDbContext(string[] args)
    {
        string currentDirectory = Directory.GetCurrentDirectory();
        const string relativePath = "../StoronnimV.Api";
        
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(currentDirectory, relativePath))
            .AddJsonFile("appsettings.json")
            .Build();

        string? connectionString = configuration.GetConnectionString("CloudConnection");

        var optionsBuilder = new DbContextOptionsBuilder<StoronnimVContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new StoronnimVContext(optionsBuilder.Options);
    }
}