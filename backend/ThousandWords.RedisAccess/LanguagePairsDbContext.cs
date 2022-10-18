using ATI.Services.Common.Behaviors;
using ATI.Services.Common.Caching.Redis;
using ThousandWords.Core.Interfaces.DbContexts;
using ThousandWords.Core.Models;

namespace ThousandWords.RedisAccess;

public class LanguagePairsDbContext : ILanguagePairsDbContext
{
    
    private const string MetricEntity = "redis";
    
    private readonly RedisCache _redisCache;

    public LanguagePairsDbContext(RedisProvider redisProvider)
    {
        _redisCache = redisProvider.GetCache(CacheNames.LanguageDictionaries.ToString());
    }

    public Task<OperationResult<List<LanguagePair>>> GetManyAsync(List<string> keys)
    {
        return _redisCache.GetManyAsync<LanguagePair>(keys, MetricEntity);
    }

    public Task<OperationResult> InsertManyAsync(IEnumerable<LanguagePair> languagePairs)
    {
        return _redisCache.InsertManyAsync(languagePairs.ToList(), MetricEntity);
    }
}