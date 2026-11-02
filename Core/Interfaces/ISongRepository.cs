using Core.Models;
namespace Core.Interfaces;

public interface ISongRepository
{
    int CreateSong(Song song);
    IReadOnlyList<Song> GetSongs();
    //int UpdateSong(Song song);
    int DeleteSong(int id);
}