using System.Security.Claims;
using Core.Services;
using Microsoft.AspNetCore.Authentication;
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
    [ValidateAntiForgeryToken]
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

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.Username),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, user.Role)
        };

        var identity = new ClaimsIdentity(
            claims,
            "SoundlyCookie");

        await HttpContext.SignInAsync(
            "SoundlyCookie",
            new ClaimsPrincipal(identity));

        return RedirectToAction("Index", "Home");
    }
    [HttpPost("logout")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync("SoundlyCookie");
        return RedirectToAction(nameof(Login));
    }
}
