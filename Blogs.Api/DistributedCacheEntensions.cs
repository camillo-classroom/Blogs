using Microsoft.Extensions.Caching.Distributed;

namespace Blogs.Api;

public static class DistributedCacheEntensions
{
    public static async Task<T?> SetCacheAndReturnObjectAsync<T>(this IDistributedCache cache, string key, T obj, int expirationInSeconds = 60)
    {
        if (obj == null)
            return default;

        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(expirationInSeconds)
        };

        var json = System.Text.Json.JsonSerializer.Serialize(obj);

        await cache.SetStringAsync(key, json, options);

        return obj;
    }
}
