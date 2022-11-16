using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Task.Application.CommandsQueries.User.Commands.Create;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserResult>
{
    private readonly UserManager<Domain.User> _userManager;
    private readonly SignInManager<Domain.User> _signInManager;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(UserManager<Domain.User> userManager, IMapper mapper, 
        SignInManager<Domain.User> signInManager)
    {
        _userManager = userManager;
        _mapper = mapper;
        _signInManager = signInManager;
    }

    public async Task<CreateUserResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var isUserExist = await _userManager.FindByEmailAsync(request.Email);

        if (isUserExist is not null)
            return CreateUserResult.Failure;

        var user = _mapper.Map<Domain.User>(request);
        user.RegistrationDate = DateTime.UtcNow;
        user.LastLoginDate = DateTime.UtcNow;
        user.Status = string.Empty;

        await _userManager.CreateAsync(user);
        await _signInManager.SignInAsync(user, false);

        return CreateUserResult.Successfully;
    }
}