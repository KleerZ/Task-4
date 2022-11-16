using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Task.Application.CommandsQueries.User.Queries.Login;

public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, LoginResult>
{
    private readonly UserManager<Domain.User> _userManager;
    private readonly SignInManager<Domain.User> _signInManager;

    public LoginUserQueryHandler(UserManager<Domain.User> userManager, 
        SignInManager<Domain.User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    
    public async Task<LoginResult> Handle(LoginUserQuery request, 
        CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        var isCorrectPassword = await _userManager.CheckPasswordAsync(user, request.Password);

        if (user is null && !isCorrectPassword)
            return LoginResult.Failure;
        
        await _signInManager.SignInAsync(user, false);

        return LoginResult.Successfully;
    }
}