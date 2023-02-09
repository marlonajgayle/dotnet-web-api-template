using Microsoft.Extensions.Caching.Memory;
using Net7WebApiTemplate.Application.Shared.Interface;

namespace Net7WebApiTemplate.Infrastructure.Cache.InMemory
{
    public class InMemoryCacheProvider : ICacheProvider
    {
        private readonly IMemoryCache _memoryCache;

        public InMemoryCacheProvider(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public void ClearCache(string key)
        {
            _memoryCache.Remove(key);
        }

        public T? GetFromCache<T>(string cacheKey) where T : class
        {
            _memoryCache.TryGetValue(cacheKey, out T? cachedResponse);

            return cachedResponse as T;
        }

        public void SetCache<T>(string key, T value, DateTimeOffset duration) where T : class
        {
            _memoryCache.Set(key, value, duration);
        }

        public void SetCache<T>(string key, T value, MemoryCacheEntryOptions options) where T : class
        {
            _memoryCache.Set(key, value, options);
        }
    }
}