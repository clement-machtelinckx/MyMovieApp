using MovieAppApi.Src.Models.Users;

namespace MovieAppApi.Src.Services.Users;

public interface IUserRepository
{
IEnumerable<User> GetUsers(GetUsersInput input);

  User? GetUserById(int userId);
}