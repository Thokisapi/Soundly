using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Soundly.Models;

namespace Soundly.Controllers;

[Route("songs")]
public class SongsController : Controller
{
    private readonly SongService _songService;

    public SongsController(SongService songService)
    {
        _songService = songService;
    }

    [HttpGet("create")]
    public IActionResult Create()
    {
        return View(new SongViewModel());
    }

    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    public IActionResult Create(SongViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        _songService.CreateSong(
            model.Title,
            model.Artist,
            model.Album,
            model.Genre,
            model.ReleaseDate);

        return RedirectToAction(nameof(Create));
    }

    [HttpPost("")]
    public IActionResult CreateSong(SongViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var songId = _songService.CreateSong(
            model.Title,
            model.Artist,
            model.Album,
            model.Genre,
            model.ReleaseDate);

        return Ok(new
        {
            Id = songId
        });
    }
}
