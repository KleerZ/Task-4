namespace Task.Application.Interfaces;

public interface IApplicationContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}