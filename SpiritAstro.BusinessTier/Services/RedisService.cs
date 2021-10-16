using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace SpiritAstro.BusinessTier.Services
{
    public interface IRedisService
    {
        Task CacheToRedis<T>(string key, T data, TimeSpan timeSpan);
        Task<T> GetFromRedis<T>(string key);
    }
    
    public class RedisService : IRedisService
    {
        private readonly IDistributedCache _distributedCache;

        public RedisService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task CacheToRedis<T>(string key, T data, TimeSpan timeSpan)
        {
            var options = new DistributedCacheEntryOptions().SetSlidingExpiration(timeSpan);
            var dataString = JsonSerializer.Serialize(data);
            await _distributedCache.SetStringAsync(key, dataString, options);
        }

        public async Task<T> GetFromRedis<T>(string key)
        {
            var dataString = await _distributedCache.GetStringAsync(key);
            
            if (dataString == null)
            {
                return default;
            }
            
            var data = JsonSerializer.Deserialize<T>(dataString);
            return data;
        }
    }
}