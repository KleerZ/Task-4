using MediatR;

namespace Task.Application.CommandsQueries.User.Commands.Block;

public class BlockUsersCommand : IRequest
{
    public long? CurrentUserId { get; set; }
    public IEnumerable<long> CheckedUsers { get; set; }
}