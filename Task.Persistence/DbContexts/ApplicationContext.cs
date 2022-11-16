using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Task.Application.Interfaces;
using Task.Domain;

namespace Task.Persistence.DbContexts;

public class ApplicationContext : IdentityDbContext<User, IdentityRole<long>, long>,
    IApplicationContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
}