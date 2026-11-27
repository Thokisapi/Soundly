using Core.Interfaces;
using Core.Models;
using MySql.Data.MySqlClient;

namespace Datalayer;

public class PlaylistRepository : IPlaylistRepository
{
    private readonly string _connectionString;
    
    public PlaylistRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public int CreatePlaylist(Playlist playlist)
    {
        const string sql = """
                           insert into playlist (name, user_id, song_id)
                            values (@name, @userId, @songId);
                           """;
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();
        using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@name", playlist.Name);
        command.Parameters.AddWithValue("@userId", playlist.UserId);
        command.Parameters.AddWithValue("@songId", playlist.SongId);
        return (int)command.ExecuteScalar();
    }
    public int DeletePlaylist(int id)
    {
        const string sql = """
                           delete from playlist where id = @id;
                           """;
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();
        using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@id", id);
        return (int)command.ExecuteScalar();
    }
    public int addSongToPlaylist(int playlistId, int songId)
    {
        const string sql = """
                           insert into playlist_songs (playlist_id, song_id)
                            values (@playlistId, @songId);
                           """;
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();
        using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@playlistId", playlistId);
        command.Parameters.AddWithValue("@songId", songId);
        return (int)command.ExecuteScalar();
    }
}