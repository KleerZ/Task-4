using MediatR;

namespace Task.Application.CommandsQueries.User.Commands.Delete;

public class DeleteUserCommand : IRequest
{
    public long? CurrentUserId { get; set; }
    public IEnumerable<long> CheckedUsers { get; set; }
}