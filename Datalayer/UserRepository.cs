using Core.Models;
using Core.Interfaces;
using MySql.Data.MySqlClient;
namespace Datalayer;

public class UserRepository : IUserRepository
{
    private readonly string _connectionString;
    public UserRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public int CreateUser(User user)
    {
        const string sql = """ INSERT INTO user (username, email, hashed_password, role) VALUES (@username, @email, @hashedPassword, @role) """;
        using var connection = new MySqlConnection(_connectionString);
        
        connection.Open();
        using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@username", user.Username);
        command.Parameters.AddWithValue("@email", user.Email);
        command.Parameters.AddWithValue("@hashedPassword", user.HashedPassword);
        command.Parameters.AddWithValue("@role", user.Role);
        return command.ExecuteNonQuery();
    }

    public User? GetUserByEmail(string email)
    {
        const string sql = """
                           SELECT id, username, email, hashed_password, role
                           FROM `user`
                           WHERE email = @email
                           """;

        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@email", email);

        using var reader = command.ExecuteReader();

        if (!reader.Read())
        {
            return null;
        }

        return new User
        {
            Id = reader.GetInt32(reader.GetOrdinal("id")),
            Username = reader.GetString(reader.GetOrdinal("username")),
            Email = reader.GetString(reader.GetOrdinal("email")),
            HashedPassword = reader.GetString(reader.GetOrdinal("hashed_password")),
            Role = reader.GetString(reader.GetOrdinal("role"))
        };
    }
}