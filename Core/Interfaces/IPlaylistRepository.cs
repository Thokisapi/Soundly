using Core.Models;

namespace Core.Interfaces;

public interface IPlaylistRepository
{
     int CreatePlaylist(Playlist playlist );
     int DeletePlaylist(int id);
     int addSongToPlaylist(int playlistId, int songId);
}