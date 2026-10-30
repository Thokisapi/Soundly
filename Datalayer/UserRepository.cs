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
}