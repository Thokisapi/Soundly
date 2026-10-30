using Core.Models;

namespace Core.Interfaces;

public interface IUserRepository
{
    int CreateUser(User user);
}
