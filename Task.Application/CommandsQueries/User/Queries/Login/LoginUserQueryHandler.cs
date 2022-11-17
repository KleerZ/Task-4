using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Task.Application.Common.Constants;

namespace Task.Application.CommandsQueries.User.Queries.Login;

public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, ModelStateDictionary>
{
    private readonly UserManager<Domain.User> _userManager;
    private readonly SignInManager<Domain.User> _signInManager;

    public LoginUserQueryHandler(UserManager<Domain.User> userManager,
        SignInManager<Domain.User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<ModelStateDictionary> Handle(LoginUserQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        var isCorrectPassword = await _userManager.CheckPasswordAsync(user, request.Password);

        if (user is null || !isCorrectPassword)
        {
            request.ModelState.AddModelError("UserIsNotExist", "There is no user with this data");
            return request.ModelState;
        }

        if (user.Status == UserStatus.Blocked)
        {
            request.ModelState.AddModelError("UserIsBlocked", "This User is blocked");
            return request.ModelState;
        }

        await _signInManager.SignInAsync(user, true);

        return request.ModelState;
    }
}