using MediatR;
using Microsoft.AspNetCore.Identity;
using Task.Application.Common.Constants;
using Task.Application.Interfaces;

namespace Task.Application.CommandsQueries.User.Commands.UnBlock;

public class UnblockUsersCommandHandler : IRequestHandler<UnblockUsersCommand, Unit>
{
    private readonly IApplicationContext _context;
    private readonly SignInManager<Domain.User> _signInManager;
    private readonly UserManager<Domain.User> _userManager;

    public UnblockUsersCommandHandler(IApplicationContext context,
        SignInManager<Domain.User> signInManager, UserManager<Domain.User> userManager)
    {
        _context = context;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public async Task<Unit> Handle(UnblockUsersCommand request, CancellationToken cancellationToken)
    {
        var users = _context.Users
            .Where(user => request.CheckedUsers.Contains(user.Id));

        foreach (var user in users)
            user.Status = UserStatus.Default;
        
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}