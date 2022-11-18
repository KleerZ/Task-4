using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Task.Application.CommandsQueries.User.Commands.Create;
using Task.Application.Common.Mappings;

namespace Task.Mvc.Models.User;

public class RegistrationViewModel : IMapWith<CreateUserCommand>
{
    [Required(ErrorMessage = "Username is required")]
    [MaxLength(100, ErrorMessage = "Max length - 100")]
    public string Name { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; } 
    
    [Required(ErrorMessage = "Please confirm your password")]
    [Compare("Password", ErrorMessage = "Passwords don't match")]
    public string PasswordConfirm { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<RegistrationViewModel, CreateUserCommand>()
            .ForMember(u => u.Name,
                o => o.MapFrom(u => u.Name))
            .ForMember(u => u.Email,
                o => o.MapFrom(u => u.Email))
            .ForMember(u => u.Password,
                o => o.MapFrom(u => u.Password));
    }
}