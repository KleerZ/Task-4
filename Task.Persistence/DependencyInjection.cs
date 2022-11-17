using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Task.Application.Interfaces;
using Task.Persistence.DbContexts;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Task.Domain;

namespace Task.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services,
        IConfiguration configuration)
    {
        string connectionString;
        
        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            connectionString = configuration["DbConnection"];
        else
            connectionString = GetConnectionString();

        services.AddDbContext<ApplicationContext>(options =>
        {
            options.UseNpgsql(connectionString,
                project => project.MigrationsAssembly("WebApp.Persistence"));
        });
        
        services.AddIdentity<User, IdentityRole<long>>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 1;
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = false;
        }).AddEntityFrameworkStores<ApplicationContext>();

        services.AddScoped<IApplicationContext, ApplicationContext>();

        return services;
    }

    private static string GetConnectionString()
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
}