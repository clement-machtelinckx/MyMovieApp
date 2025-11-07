using Microsoft.AspNetCore.Mvc;
using MovieAppApi.Src.Services.Users;
using MovieAppApi.Src.DTO;
using MovieAppApi.Src.Models.Users;

namespace MovieAppApi.Src.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
  private readonly IUserService _userService;

  public UsersController(IUserService userService)
  {
    _userService = userService;
  }

  [HttpGet]
  public IActionResult GetUsers([FromQuery] GetUsersSearchParams? searchParams)
  {
    var input = new GetUsersInput
    {
      MinAge = searchParams?.MinAge,
      MaxAge = searchParams?.MaxAge
    };

    var models = _userService.GetUsers(input);

    var dto = models.Select(u => new GetUserResponseDto
    (
      id: u.Id,
      username: u.Username,
      age: u.Age
    ));

    return Ok(dto);
  }

  [HttpGet("age-average")]
  public IActionResult GetAgeAverage()
  {
    var averageAge = _userService.GetAverageAge();

    return Ok(averageAge);
  }

  [HttpGet("{userId}")]
  public IActionResult GetUserById(int userId)
  {
    var model = _userService.GetUserById(userId);

    if (model == null)
    {
      return NotFound();
    }

    var dto = new GetUserResponseDto
    (
      id: model.Id,
      username: model.Username,
      age: model.Age
    );

    return Ok(dto);
  }
}