using Microsoft.EntityFrameworkCore;
using Task.Domain;

namespace Task.Application.Interfaces;

public interface IApplicationContext
{
    public DbSet<User> Users { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}