using UserService.Services;
using UserService.UserService.Data.Models;

public class UsersService
{
    private readonly MongoDbService _mongoDbService;
    private readonly RedisCacheService _redisCacheService;

    public UsersService(MongoDbService mongoDbService, RedisCacheService redisCacheService)
    {
        _mongoDbService = mongoDbService;
        _redisCacheService = redisCacheService;
    }

    public async Task CreateUserAsync(User user)
    {
        // Сохраняем пользователя в MongoDB
        await _mongoDbService.CreateUserAsync(user);

        // Кешируем данные в Redis
        await _redisCacheService.SetCacheAsync(user.Id, user.FullName);
    }

    public async Task<User> GetUserAsync(string userId)
    {
        // Сначала проверяем Redis
        var cachedUser = await _redisCacheService.GetCacheAsync(userId);
        if (cachedUser != null)
        {
            return new User { Id = userId, FullName = cachedUser };
        }

        // Если в Redis нет данных, получаем из MongoDB
        var user = await _mongoDbService.GetUserByIdAsync(userId);
        if (user != null)
        {
            // Сохраняем в Redis
            await _redisCacheService.SetCacheAsync(userId, user.FullName);
        }

        return user;
    }
}
