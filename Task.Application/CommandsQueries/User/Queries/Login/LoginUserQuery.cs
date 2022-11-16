using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Task.Application.CommandsQueries.User.Queries.Login;

public class LoginUserQuery : IRequest<ModelStateDictionary>
{
    public string Email { get; set; }
    public string Password { get; set; }
    
    public ModelStateDictionary ModelState { get; set; }
}