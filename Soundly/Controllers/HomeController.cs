using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Soundly.Controllers;

public class HomeController : Controller
{
    private readonly SongService _songService;

    public HomeController(SongService songService)
    {
        _songService = songService;
    }

    public IActionResult Index()
    {
        var songs = _songService.GetSongs();

        return View(songs);
    }
}
