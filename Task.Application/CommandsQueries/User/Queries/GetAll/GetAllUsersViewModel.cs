namespace Task.Application.CommandsQueries.User.Queries.GetAll;

public class GetAllUsersViewModel
{
    public IEnumerable<Domain.User> Users { get; set; }
}