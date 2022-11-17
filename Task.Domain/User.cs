using Microsoft.AspNetCore.Identity;

namespace Task.Domain;

public class User : IdentityUser<long>
{
    public string Name { get; set; }
    public DateTime RegistrationDate { get; set; }
    public DateTime LastLoginDate { get; set; }
    public string? Status { get; set; } 
}