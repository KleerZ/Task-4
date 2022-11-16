using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Task.Persistence.DbContexts;

public class ApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
{
    public ApplicationContext CreateDbContext(string[] args)
    {
        var path = Directory.GetCurrentDirectory().Replace("Persistence", "Mvc");
        var configuration = new ConfigurationBuilder()
            .SetBasePath(path)
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<ApplicationContext>();
        var connectionString = configuration["DbConnection"];

        builder.UseNpgsql(connectionString, b => b.MigrationsAssembly("Task.Persistence"));

        return new ApplicationContext(builder.Options);
    }
}