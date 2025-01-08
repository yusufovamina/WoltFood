namespace UserService.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using global::UserService.Models.DTO;
using global::UserService.Models;

public interface IUserService
{
    Task<string> Authenticate(UserLoginDto userLogin);
    Task<bool> Register(UserRegisterDto userRegister);
}

public class UserService : IUserService
{
    private readonly List<User> _users = new List<User>(); // Это пример. В реальном приложении используйте БД.
    private readonly IConfiguration _configuration;

    public UserService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<string> Authenticate(UserLoginDto userLogin)
    {
        // Найдем пользователя в "базе данных"
        var user = _users.SingleOrDefault(u => u.Username == userLogin.Username && u.Password == userLogin.Password);

        if (user == null)
        {
            return null; // Неудачная авторизация
        }

        // Генерируем токен
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new System.Security.Claims.ClaimsIdentity(new[]
            {
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, user.Username),
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.NameIdentifier, user.Id.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public async Task<bool> Register(UserRegisterDto userRegister)
    {
        // Проверим, есть ли уже такой пользователь
        if (_users.Any(u => u.Username == userRegister.Username))
        {
            return false; // Пользователь уже существует
        }

        var user = new User
        {
            Id = _users.Count + 1,
            Username = userRegister.Username,
            Password = userRegister.Password
        };

        _users.Add(user); // Сохраним пользователя в "базу данных"
        return true;
    }
}
