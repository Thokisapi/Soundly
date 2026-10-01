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

    public async Task<IActionResult> Index()
    {
        var songs = await _songRepository.GetSongsAsync();

        return View(songs);
    }
}
