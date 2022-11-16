using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Task.Application.Interfaces;
using Task.Persistence.DbContexts;

namespace Task.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration["DbConnection"];
        
        services.AddDbContext<ApplicationContext>(options =>
        {
            options.UseNpgsql(connectionString,
                b => b.MigrationsAssembly("WebApp.Persistence"));
        });

        services.AddScoped<IApplicationContext, ApplicationContext>();

        return services;
    }
}