using Microsoft.Extensions.Configuration;

namespace Task.Application.Common;

public class DbConnectionString
{
    public string GetFromVariables()
    {
        var connectionUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

        connectionUrl = connectionUrl!.Replace("postgres://", string.Empty);
        var userPassSide = connectionUrl.Split("@")[0];
        var hostSide = connectionUrl.Split("@")[1];

        var user = userPassSide.Split(":")[0];
        var password = userPassSide.Split(":")[1];
        var host = hostSide.Split("/")[0];
        var database = hostSide.Split("/")[1].Split("?")[0];

        return $"Host={host};Database={database};Username={user};Password={password};SSL Mode=Require;Trust Server Certificate=true";
    }
    
    public string GetFromAppSettings(string key)
    {
        var path = Directory.GetCurrentDirectory().Replace("Persistence", "Mvc");
        var configuration = new ConfigurationBuilder()
            .SetBasePath(path)
            .AddJsonFile("appsettings.json")
            .Build();
        
        return configuration[key];
    }
}