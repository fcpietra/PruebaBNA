using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using PruebaBNA.Application.Common.Interfaces;
using PruebaBNA.Application.Common.Settings;

namespace PruebaBNA.Infrastructure.Services;

public class MemoryCacheService : ICacheService
{
    private readonly IMemoryCache _memoryCache;
    private readonly CacheSettings _cacheSettings;

    public MemoryCacheService(IMemoryCache memoryCache, IOptions<CacheSettings> cacheSettings)
    {
        _memoryCache = memoryCache;
        _cacheSettings = cacheSettings.Value; // Accedemos a los valores configurados
    }

    public T? Get<T>(string key)
    {
        _memoryCache.TryGetValue(key, out T? value);
        return value;
    }

    public void Set<T>(string key, T value)
    {
        var cacheOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_cacheSettings.ExpirationInMinutes),
            SlidingExpiration = TimeSpan.FromSeconds(30) // Opcional: refrescar si se usa mucho
        };

        _memoryCache.Set(key, value, cacheOptions);
    }

    public void Remove(string key)
    {
        _memoryCache.Remove(key);
    }
}