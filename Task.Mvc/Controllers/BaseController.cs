using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Task.Mvc.Controllers;

public class BaseController : Controller
{
    private IMediator? _mediator;

    protected IMediator Mediator =>
        _mediator ?? HttpContext.RequestServices.GetService<IMediator>()!;

    internal long? UserId => User.Identity!.IsAuthenticated
        ? Convert.ToInt64(User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
        : null;
}