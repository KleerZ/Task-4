using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Task.Application.Common;
using Task.Application.Interfaces;
using Task.Persistence.DbContexts;
using Task.Domain;

namespace Task.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services,
        IConfiguration configuration)
    {
        string connectionString;

        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
            connectionString = DbConnectionString.GetFromVariables();
        else
            connectionString = DbConnectionString.GetFromAppSettings("DbConnection");

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
}