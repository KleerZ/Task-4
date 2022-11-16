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
        query.ModelState = ModelState;
        
        var modelState = await _mediator.Send(query);

        if (!modelState.IsValid)
            return View(model);

        return !modelState.IsValid ? View(model) : RedirectToAction("Index", "UserManagement");
    }
}