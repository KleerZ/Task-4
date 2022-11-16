using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Task.Application.Interfaces;

namespace Task.Application.CommandsQueries.User.Queries.GetAll;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, GetAllUsersViewModel>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;

    public GetAllUsersQueryHandler(IApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<GetAllUsersViewModel> Handle(GetAllUsersQuery request,
        CancellationToken cancellationToken)
    {
        var users = await _context.Users
            .ToListAsync(cancellationToken);

        return new GetAllUsersViewModel { Users = users };
    }
}