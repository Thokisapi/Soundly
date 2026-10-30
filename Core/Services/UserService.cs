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
}
