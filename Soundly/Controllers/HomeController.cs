using Datalayer;
using Microsoft.AspNetCore.Mvc;

namespace Soundly.Controllers;

public class HomeController : Controller
{
    private readonly SongRepository _songRepository;

    public HomeController(SongRepository songRepository)
    {
        _songRepository = songRepository;
    }

    public IActionResult Index()
    {
        var songs = _songRepository.GetSongs();

        return View(songs);
    }
}
