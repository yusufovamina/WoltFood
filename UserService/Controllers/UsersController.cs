using Microsoft.AspNetCore.Mvc;
using UserService.Services;
using UserService.UserService.Data.Models.DTO;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegister)
    {
        var result = await _userService.Register(userRegister);

        if (!result)
        {
            return BadRequest("User already exists.");
        }

        return Ok("User registered successfully.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto userLogin)
    {
        var token = await _userService.Authenticate(userLogin);

        if (string.IsNullOrEmpty(token))
        {
            return Unauthorized("Invalid username or password.");
        }

        return Ok(new { Token = token });
    }
}
