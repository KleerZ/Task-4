using AutoMapper;
using MediatR;
using Task.Application.Common.Mappings;

namespace Task.Application.CommandsQueries.User.Commands.Create;

public class CreateUserCommand : IRequest<CreateUserResult>, IMapWith<Domain.User>
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateUserCommand, Domain.User>()
            .ForMember(u => u.UserName,
                o => o.MapFrom(u => u.UserName))
            .ForMember(u => u.Email,
                o => o.MapFrom(u => u.Email));
    }
}