using MediatR;
using Task.Application.Common.Mappings;

namespace Task.Application.CommandsQueries.User.Queries.Login;

public class LoginUserQuery : IRequest<LoginResult>
{
    public string Email { get; set; }
    public string Password { get; set; }
}