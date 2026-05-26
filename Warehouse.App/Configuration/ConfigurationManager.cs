using Microsoft.Extensions.Configuration;

namespace Warehouse.App.Configuration;

public static class ConfigurationManager
{
    static ConfigurationManager()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
        Configuration = builder.Build();
        ConnectionString = Configuration.GetConnectionString("DefaultConnection")!;
        if (string.IsNullOrEmpty(ConnectionString))
            throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured in appsettings.json.");
    }

    public static IConfiguration Configuration { get; }

    public static string ConnectionString { get; }
}