using Microsoft.Extensions.Caching.Distributed;

namespace UserService.Services;
public class RedisCacheService
{
    private readonly IDistributedCache _cache;

    public RedisCacheService(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task SetCacheAsync(string key, string value)
    {
        await _cache.SetStringAsync(key, value);
    }

    public async Task<string> GetCacheAsync(string key)
    {
        return await _cache.GetStringAsync(key);
    }
}
