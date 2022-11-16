using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Task.Application.CommandsQueries.User.Commands.Create;
using Task.Mvc.Models.User;

namespace Task.Mvc.Controllers.User;

public class RegistrationController : Controller
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public RegistrationController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(RegistrationViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var command = _mapper.Map<CreateUserCommand>(model);
        var result = await _mediator.Send(command);

        if (result is not CreateUserResult.Successfully)
        {
            ModelState.AddModelError("UserIsExist", "User with this email already exist");
            return View(model);
        }

        return RedirectToAction("Index", "Home");
    }
}