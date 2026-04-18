using Microsoft.Extensions.Caching.Distributed;
using SatisTalepYonetimi.Application.Services;
using System.Text.Json;

namespace SatisTalepYonetimi.Infrastructure.Services;

public sealed class RedisCacheService(IDistributedCache distributedCache) : ICacheService
{
    private static readonly TimeSpan DefaultExpiration = TimeSpan.FromMinutes(5);

    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        var cachedValue = await distributedCache.GetStringAsync(key, cancellationToken);
        if (cachedValue is null)
            return default;

        return JsonSerializer.Deserialize<T>(cachedValue);
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null, CancellationToken cancellationToken = default)
    {
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiration ?? DefaultExpiration
        };

        var serializedValue = JsonSerializer.Serialize(value);
        await distributedCache.SetStringAsync(key, serializedValue, options, cancellationToken);
    }

    public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        await distributedCache.RemoveAsync(key, cancellationToken);
    }

    public Task RemoveByPrefixAsync(string prefixKey, CancellationToken cancellationToken = default)
    {
        // Redis'te prefix bazlı silme için StackExchange.Redis doğrudan kullanılabilir
        // Basit implementasyon: tek key silme
        return distributedCache.RemoveAsync(prefixKey, cancellationToken);
    }
}
