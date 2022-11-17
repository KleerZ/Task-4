using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Task.Application.Common;

namespace Task.Persistence.DbContexts;

public class ApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
{
    public ApplicationContext CreateDbContext(string[] args)
    {
        string connectionString;
        var dbConnectionString = new DbConnectionString();

        var builder = new DbContextOptionsBuilder<ApplicationContext>();

        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
            connectionString = dbConnectionString.GetFromVariables();
        else
            connectionString = dbConnectionString.GetFromAppSettings("DbConnection");

        builder.UseNpgsql(connectionString, 
            options => options.MigrationsAssembly("Task.Persistence"));

        return new ApplicationContext(builder.Options);
    }
}