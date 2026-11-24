using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Identity;

namespace Core.Services;

public class UserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;

    public UserService(
        IUserRepository userRepository,
        IPasswordHasher<User> passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public void RegisterUser(string username, string email, string password)
    {
        var user = new User
        {
            Username = username,
            Email = email,
            Role = "user"
        };
        user.HashedPassword = _passwordHasher.HashPassword(user, password);
        _userRepository.CreateUser(user);
    }

    public User? Login(string email, string password)
    {
        var user = _userRepository.GetUserByEmail(email);
        if (user is null)
        {
            return null;
        }

        var result = _passwordHasher.VerifyHashedPassword(
            user,
            user.HashedPassword,
            password);

        if (result == PasswordVerificationResult.Failed)
        {
            return null;
        }

        return user;
    }
}
