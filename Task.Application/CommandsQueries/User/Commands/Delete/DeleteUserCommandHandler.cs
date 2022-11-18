using MediatR;
using Microsoft.AspNetCore.Identity;
using Task.Application.Interfaces;

namespace Task.Application.CommandsQueries.User.Commands.Delete;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
{
    private readonly IApplicationContext _context;
    private readonly SignInManager<Domain.User> _signInManager;
    private readonly UserManager<Domain.User> _userManager;

    public DeleteUserCommandHandler(IApplicationContext context,
        SignInManager<Domain.User> signInManager, UserManager<Domain.User> userManager)
    {
        _context = context;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var users = _context.Users
            .Where(user => request.CheckedUsers.Contains(user.Id));

        _context.Users.RemoveRange(users);

        if (users.Contains(new Domain.User { Id = request.CurrentUserId.Value }))
            await _signInManager.SignOutAsync();

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}