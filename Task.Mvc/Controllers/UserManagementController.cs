using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Task.Application.CommandsQueries.User.Commands.Block;
using Task.Application.CommandsQueries.User.Commands.Delete;
using Task.Application.CommandsQueries.User.Commands.UnBlock;
using Task.Application.CommandsQueries.User.Queries.GetAll;
using Task.Application.Common.Filters;
using Task.Domain;

namespace Task.Mvc.Controllers;

[ServiceFilter(typeof(StatusValidationFilter))]
[Authorize]
public class UserManagementController : BaseController
{
    private readonly IMediator _mediator;
    private readonly SignInManager<User> _signInManager;

    public UserManagementController(IMediator mediator,
        SignInManager<User> signInManager)
    {
        _mediator = mediator;
        _signInManager = signInManager;
    }

    [HttpGet]
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
    
    [HttpGet]
    public async Task<IActionResult> SignOut()
    {
        await _signInManager.SignOutAsync();
        
        return RedirectToAction("Index", "Login");
    }
}