using Core.Interfaces;
using Core.Models;

namespace Core.Services;

public class SongService
{
    private readonly ISongRepository _songRepository;

    public SongService(ISongRepository songRepository)
    {
        _songRepository = songRepository;
        
    }

    public int CreateSong(string title, string artist, string album, string genre, DateOnly releaseDate)
    {
        var song = new Song
        {
            Title = title,
            Artist = artist,
            Album = album,
            Genre = genre,
            ReleaseDate = releaseDate
        };
        return _songRepository.CreateSong(song);
    }

    public IReadOnlyList<Song> GetSongs()
    {
        return _songRepository.GetSongs();
    }
}