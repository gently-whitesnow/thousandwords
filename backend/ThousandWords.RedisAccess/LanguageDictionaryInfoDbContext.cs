using ATI.Services.Common.Behaviors;
using ATI.Services.Common.Caching.Redis;
using ATI.Services.Common.Initializers.Interfaces;
using ThousandWords.Core.Interfaces.DbContexts;
using ThousandWords.Core.Models;

namespace ThousandWords.RedisAccess;

public class LanguageDictionaryInfoDbContext : ILanguageDictionaryInfoDbContext
{
    private const string MetricEntity = "redis";
    
    private readonly RedisCache _redisCache;

    public LanguageDictionaryInfoDbContext(RedisProvider redisProvider)
    {
        _redisCache = redisProvider.GetCache(CacheNames.LanguageDictionaries.ToString());
    }
    
    public Task<OperationResult<LanguageDictionaryInfo>> GetLanguageDictionaryByNameAsync(string name)
    {
        return _redisCache.GetAsync<LanguageDictionaryInfo>(name, MetricEntity);
    }

    public Task<OperationResult> InsertAsync(LanguageDictionaryInfo info)
    {
        return _redisCache.InsertAsync(info, MetricEntity);
    }
}