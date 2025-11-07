using MyWebAPI.Models;

namespace MyWebAPI.Services.User;

public interface IUserService
{
  ICollection<User> GetAllUsers();
  double GetAverageAge();
}