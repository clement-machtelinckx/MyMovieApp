using MovieAppApi.Src.Models.Users;

namespace MovieAppApi.Src.Services.Users;

public interface IUserService
{
  ICollection<User> GetAllUsers();
  double GetAverageAge();
}