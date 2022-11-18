using MediatR;
using Microsoft.AspNetCore.Identity;
using Task.Application.Common.Constants;
using Task.Application.Interfaces;

namespace Task.Application.CommandsQueries.User.Commands.Block;

public class BlockUserCommandHandler : IRequestHandler<BlockUsersCommand, Unit>
{
    private readonly IApplicationContext _context;
    private readonly SignInManager<Domain.User> _signInManager;
    private readonly UserManager<Domain.User> _userManager;

    public BlockUserCommandHandler(IApplicationContext context,
        SignInManager<Domain.User> signInManager, UserManager<Domain.User> userManager)
    {
        _context = context;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public async Task<Unit> Handle(BlockUsersCommand request,
        CancellationToken cancellationToken)
    {
        var users = _context.Users
            .Where(user => request.CheckedUsers.Contains(user.Id));

        foreach (var user in users)
        {
            user.Status = UserStatus.Blocked;

            if (user.Id == request.CurrentUserId)
                await _signInManager.SignOutAsync();
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}