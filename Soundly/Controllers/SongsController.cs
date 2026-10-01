using Core.Models;
using Datalayer;
using Microsoft.AspNetCore.Mvc;

namespace Soundly.Controllers;

[Route("songs")]
public class SongsController : Controller
{
    private readonly SongRepository _songRepository;

    public SongsController(SongRepository songRepository)
    {
        _songRepository = songRepository;
    }

    [HttpGet("create")]
    public IActionResult Create()
    {
        return View(new Song());
    }

    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Song song)
    {
        if (!ModelState.IsValid)
        {
            return View(song);
        }

        await _songRepository.CreateSongAsync(song);

        return RedirectToAction(nameof(Create));
    }

    [HttpPost("")]
    public async Task<IActionResult> CreateSong(Song song)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var songId = await _songRepository.CreateSongAsync(song);

        return Ok(new
        {
            Id = songId
        });
    }
}
