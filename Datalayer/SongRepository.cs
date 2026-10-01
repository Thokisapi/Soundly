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

    public async Task<int> CreateSongAsync(Song song)
    {
        const string sql = """
                           INSERT INTO song
                               (title, artist, album, genre, release_date)
                           VALUES
                               (@title, @artist, @album, @genre, @releaseDate);

                           SELECT LAST_INSERT_ID();
                           """;

        await using var connection = new MySqlConnection(_connectionString);
        await connection.OpenAsync();

        await using var command = new MySqlCommand(sql, connection);

        command.Parameters.AddWithValue("@title", song.Title);
        command.Parameters.AddWithValue("@artist", song.Artist);
        command.Parameters.AddWithValue("@album", song.Album);
        command.Parameters.AddWithValue("@genre", song.Genre);
        command.Parameters.AddWithValue("@releaseDate", song.ReleaseDate.ToDateTime(TimeOnly.MinValue));

        var result = await command.ExecuteScalarAsync();
        Console.WriteLine($"Inserted song with ID: {result}");

        return Convert.ToInt32(result);
    }

    public async Task<IReadOnlyList<Song>> GetSongsAsync()
    {
        const string sql = """
                           SELECT id, title, artist, album, genre, release_date
                           FROM song
                           ORDER BY id DESC;
                           """;

        var songs = new List<Song>();

        await using var connection = new MySqlConnection(_connectionString);
        await connection.OpenAsync();
        await using var command = new MySqlCommand(sql, connection);
        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            songs.Add(new Song
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                Title = reader.GetString(reader.GetOrdinal("title")),
                Artist = reader.GetString(reader.GetOrdinal("artist")),
                Album = reader.GetString(reader.GetOrdinal("album")),
                Genre = reader.GetString(reader.GetOrdinal("genre")),
                ReleaseDate = DateOnly.FromDateTime(
                    reader.GetDateTime(reader.GetOrdinal("release_date")))
            });
        }

        return songs;
    }
}