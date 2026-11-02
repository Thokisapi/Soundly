using Core.Interfaces;
using Core.Models;


namespace Core.Services;

public class UserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public void RegisterUser(string username, string email, string password)
    {
        var user = new User
        {
            Username = username,
            Email = email,
            HashedPassword = password,
            Role = "user"
        };

        _userRepository.CreateUser(user);
    }

    public User GetUser(string email)
    {
        var user = _userRepository.GetUserByEmail(email);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        return user;
    }
    public User? Login(string email , string password)
    {
        var user = _userRepository.GetUserByEmail(email);
        if (user == null || user.HashedPassword != password)
        {
            return null;
        }
        return new User{
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            HashedPassword = user.HashedPassword,
            Role = user.Role
        };}
    }
