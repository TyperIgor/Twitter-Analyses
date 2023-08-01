using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using TwitterAnalysis.Infrastructure.Cache.Interfaces;

namespace TwitterAnalysis.Infrastructure.Cache
{
    public class CacheService : ICacheService
    {
        public DistributedCacheEntryOptions DistributedOptions { get; set; } = new();
        public readonly IDistributedCache _cache;
        public CacheService(IDistributedCache cache) 
        {
            _cache = cache;
            DistributedOptions.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
            DistributedOptions.SlidingExpiration = TimeSpan.FromMinutes(1);    
        }

        public async Task SetCacheDataAsync<T>(string key, T data)
        {
            var jsonData = JsonSerializer.Serialize(data);

            await _cache.SetStringAsync(key, jsonData, DistributedOptions);
        }

        public async Task<string> GetAllDataTrainingInCache(string key)
        {
            string? jsonDataObject;
            try
            {
                jsonDataObject = await _cache.GetStringAsync(key);

                if (jsonDataObject is null)
                {
                    return default(string);
                }

            }
            catch (Exception e)
            {
                throw e;
            }

            return jsonDataObject;
        }
    }
}
