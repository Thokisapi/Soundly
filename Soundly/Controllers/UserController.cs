using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Soundly.Models;

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
}
