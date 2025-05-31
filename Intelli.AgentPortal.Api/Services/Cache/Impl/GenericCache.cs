using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Intelli.AgentPortal.Api.Services.Cache.Impl
{
    /// <summary>
    /// The generic cache.
    /// </summary>
    public class GenericCache<T> : IGenericCache<T>
    {
        private readonly MemoryCache _cache;
        private readonly CacheSettings _settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericCache{T}"/> class.
        /// </summary>
        public GenericCache(IOptions<CacheSettings> options)
        {
            _settings = options.Value;
            _cache = new MemoryCache(new MemoryCacheOptions { SizeLimit = _settings.SizeLimit });
        }

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public async Task SetValueAsync(int key, T value)
        {
            await Task.Run(() =>
            {
                // Build cache entry options
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                                            // Set cache entry size by extension method.
                                            .SetSize(1)
                                            // Keep in cache for this time, reset time if accessed.
                                            .SetSlidingExpiration(TimeSpan.FromMinutes(_settings.SlidingExpirationInMinutes));

                // Set absolute expiration
                cacheEntryOptions.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_settings.AbsoluteExpirationInMinutes);

                // Save data in cache.
                _cache.Set(key, value, cacheEntryOptions);
            });
        }

        /// <summary>
        /// Checks if cache has the value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>True, if has value, else false.</returns>
        public async Task<bool> HasValueAsync(int key)
        {
            return await Task.Run(() =>
            {
                var result = _cache.TryGetValue(key, out T value);
                return result;
            });
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>A value of type T.</returns>
        public async Task<T> GetValueAsync(int key)
        {
            return await Task.Run(() =>
            {
                if (_cache.TryGetValue(key, out T value))
                    return value;

                return default;
            });
        }
    }
}
