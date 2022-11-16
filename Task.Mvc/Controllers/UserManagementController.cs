using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task.Application.CommandsQueries.User.Commands.Block;
using Task.Application.CommandsQueries.User.Commands.Delete;
using Task.Application.CommandsQueries.User.Commands.UnBlock;
using Task.Application.CommandsQueries.User.Queries.GetAll;

namespace Task.Mvc.Controllers;

public class UserManagementController : BaseController
{
    private readonly IMediator _mediator;

    public UserManagementController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Index()
    {
        var query = new GetAllUsersQuery();
        var users = (await _mediator.Send(query))
            .Users.OrderBy(user => user.Id);
        
        return View(users);
    }

    [HttpPost]
    public async Task<IActionResult> Block([FromBody] long[] checkedUsers)
    {
        var command = new BlockUsersCommand
        {
            CurrentUserId = UserId,
            CheckedUsers = checkedUsers
        };

        await _mediator.Send(command);
        
        return RedirectToAction("Index", "UserManagement");
    }

    [HttpPost]
    public async Task<IActionResult> Unblock([FromBody] long[] checkedUsers)
    {
        var command = new UnblockUsersCommand { CheckedUsers = checkedUsers };
        await _mediator.Send(command);

        return RedirectToAction("Index");
    }
    
    [HttpPost]
    public async Task<IActionResult> Delete([FromBody] long[] checkedUsers)
    {
        var command = new DeleteUserCommand
        {
            CheckedUsers = checkedUsers,
            CurrentUserId = UserId
        };
        await _mediator.Send(command);

        return RedirectToAction("Index", "UserManagement");
    }
}