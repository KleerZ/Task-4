using MediatR;

namespace Task.Application.CommandsQueries.User.Queries.Get;

public class GetUserQuery : IRequest<Domain.User?>
{
    public long UserId { get; set; }
}