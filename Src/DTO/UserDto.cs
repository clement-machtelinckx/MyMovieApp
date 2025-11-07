using MovieAppApi.Src.Models.Users;

namespace MovieAppApi.Src.DTO.Users;
public class UserDto
{
  public int Id { get; init; }
  public string Username { get; init; }
  public int Age { get; init; }

  public static UserDto FromModel(User user)
  {
    return new UserDto
    {
      Id = user.Id,
      Username = user.Username,
      Age = user.Age
    };
  }
}