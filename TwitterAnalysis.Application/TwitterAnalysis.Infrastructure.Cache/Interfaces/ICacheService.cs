
using Microsoft.Extensions.Caching.Distributed;

namespace TwitterAnalysis.Infrastructure.Cache.Interfaces
{
    public interface ICacheService
    {
        Task SetCacheDataAsync<T>(string key, T data);

        Task<string> GetAllDataTrainingInCache(string key);
    }
}
