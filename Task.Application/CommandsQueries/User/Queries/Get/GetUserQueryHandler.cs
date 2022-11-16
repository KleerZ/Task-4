using MediatR;
using Microsoft.EntityFrameworkCore;
using Task.Application.Interfaces;

namespace Task.Application.CommandsQueries.User.Queries.Get;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, Domain.User?>
{
    private readonly IApplicationContext _context;

    public GetUserQueryHandler(IApplicationContext context)
    {
        _context = context;
    }

    public async Task<Domain.User?> Handle(GetUserQuery request, 
        CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

        return user;
    }
}