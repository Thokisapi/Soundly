using Core.Models;
using MySql.Data.MySqlClient;

namespace Datalayer;

public class SongRepository
{
    private readonly string _connectionString;

    public SongRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public int CreateSong(Song song)
    {
        const string sql = """
                           INSERT INTO song
                               (title, artist, album, genre, release_date)
                           VALUES
                               (@title, @artist, @album, @genre, @releaseDate);
                           """;

        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        using var command = new MySqlCommand(sql, connection);

        command.Parameters.AddWithValue("@title", song.Title);
        command.Parameters.AddWithValue("@artist", song.Artist);
        command.Parameters.AddWithValue("@album", song.Album);
        command.Parameters.AddWithValue("@genre", song.Genre);
        command.Parameters.AddWithValue("@releaseDate", song.ReleaseDate.ToDateTime(TimeOnly.MinValue));

        var result = command.ExecuteNonQuery();
        Console.WriteLine($"Inserted song with ID: {result}");

        return Convert.ToInt32(result);
    }

    public IReadOnlyList<Song> GetSongs()
    {
        const string sql = """
                           SELECT id, title, artist, album, genre, release_date
                           FROM song
                           ORDER BY id DESC;
                           """;

        var songs = new List<Song>();

        using var connection = new MySqlConnection(_connectionString);
        connection.Open();
        using var command = new MySqlCommand(sql, connection);
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            songs.Add(new Song
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                Title = reader.GetString(reader.GetOrdinal("title")),
                Artist = reader.GetString(reader.GetOrdinal("artist")),
                Album = reader.GetString(reader.GetOrdinal("album")),
                Genre = reader.GetString(reader.GetOrdinal("genre")),
                ReleaseDate = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("release_date")))
            });
        }
        return songs;
    }
}