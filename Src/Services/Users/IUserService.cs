using MovieAppApi.Src.Models.Users;

namespace MovieAppApi.Src.Services.Users;

public interface IUserService
{
  ICollection<User> GetUsers(GetUsersInput input);
  double GetAverageAge();
  User? GetUserById(int userId);
}