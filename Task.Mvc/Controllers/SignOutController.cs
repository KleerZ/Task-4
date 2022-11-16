using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Task.Domain;

namespace Task.Mvc.Controllers;

public class SignOutController : Controller
{
    private readonly SignInManager<User> _signInManager;

    public SignOutController(SignInManager<User> signInManager)
    {
        _signInManager = signInManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        await _signInManager.SignOutAsync();
        
        return RedirectToAction("Index", "Login");
    }
}