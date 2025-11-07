using MyWebAPI.Models;

namespace MyWebAPI.Services.User;

public interface IUserRepository
{
  IEnumerable<User> GetAllUsers();
}