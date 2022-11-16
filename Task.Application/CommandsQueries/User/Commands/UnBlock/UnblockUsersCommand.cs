using MediatR;

namespace Task.Application.CommandsQueries.User.Commands.UnBlock;

public class UnblockUsersCommand : IRequest
{
    public IEnumerable<long> CheckedUsers { get; set; }
}