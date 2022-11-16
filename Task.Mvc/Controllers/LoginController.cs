using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Task.Application.CommandsQueries.User.Queries.Login;
using Task.Mvc.Models.User;

namespace Task.Mvc.Controllers;

public class LoginController : Controller
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public LoginController(IMapper mapper, IMediator mediator)
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
    public async Task<IActionResult> Index(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var query = _mapper.Map<LoginUserQuery>(model);
        var result = await _mediator.Send(query);

        if (result == LoginResult.Failure)
        {
            ModelState.AddModelError("UserIsNotExist", "There is no user with this data");
            return View(model);
        }
        
        return RedirectToAction("Index", "UserManagement");
    }
}