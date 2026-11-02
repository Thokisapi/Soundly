using Core.Services;
using Soundly.Models;
using Microsoft.AspNetCore.Mvc;

namespace Soundly.Controllers;

[Route("user")]
public class UserController : Controller
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet("register")]
    public IActionResult Register()
    {
        return View(new RegisterViewModel());
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        _userService.RegisterUser(model.Username, model.Email, model.Password);
        return RedirectToAction(nameof(Register));
    }
    [HttpGet("login")]
    public IActionResult Login()
    {
        return View(new LoginViewModel());
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = _userService.Login(
            model.Email,
            model.Password);

        if (user is null)
        {
            ModelState.AddModelError(
                string.Empty,
                "Invalid email or password.");

            return View(model);
        }

        // Create the authentication cookie here.

        return RedirectToAction("Index", "Home");
    }
}
