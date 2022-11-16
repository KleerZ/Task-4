using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Task.Application.CommandsQueries.User.Queries.Login;
using Task.Application.Common.Mappings;

namespace Task.Mvc.Models.User;

public class LoginViewModel : IMapWith<LoginUserQuery>
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<LoginViewModel, LoginUserQuery>()
            .ForMember(l => l.Email,
                o => o.MapFrom(l => l.Email))
            .ForMember(l => l.Password,
                o => o.MapFrom(l => l.Password));
    }
}