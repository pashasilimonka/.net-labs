using Microsoft.Extensions.Caching.Memory;

namespace BusinessLogicLayer.Service
{
    public class CacheService
    {
        private readonly IMemoryCache _cache;
        public CacheService(IMemoryCache cache)
        {
            _cache = cache;
        }
        public T GetOrSet<T>(string key, Func<T> valueFactory, TimeSpan absoluteExpiration, TimeSpan slidingExpiration, int cacheSizeLimit)
        {
            {
                if (!_cache.TryGetValue(key, out T value))
                {
                    value = valueFactory();
                    var cacheEntryOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = absoluteExpiration,
                        SlidingExpiration = slidingExpiration,
                        Size = cacheSizeLimit
                    };
                    cacheEntryOptions.RegisterPostEvictionCallback((key, value, reason, state) =>
                    {
                        if (reason == EvictionReason.Capacity)
                        {
                            Console.WriteLine($"Entry with key {key} evicted due to capacity.");
                        }
                    });
                    _cache.Set(key, value, cacheEntryOptions);
                }
                return value;
            }
        }
    }
}
