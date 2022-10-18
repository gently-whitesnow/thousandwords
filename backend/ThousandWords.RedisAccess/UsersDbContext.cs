using ATI.Services.Common.Behaviors;
using ATI.Services.Common.Caching.Redis;
using ThousandWords.Core.Interfaces.DbContexts;
using ThousandWords.Core.Models;

namespace ThousandWords.RedisAccess;

public class UsersDbContext : IUsersDbContext
{
    private const string MetricEntity = "redis";
    
    private readonly RedisCache _redisCache;

    public UsersDbContext(RedisProvider redisProvider)
    {
        _redisCache = redisProvider.GetCache(CacheNames.Users.ToString());
    }

    public Task<OperationResult<User>> GetUserByKeyAsync(string key)
    {
        return _redisCache.GetAsync<User>(key, MetricEntity);
    }

    public Task<OperationResult> InsertAsync(User user)
    {
        return _redisCache.InsertAsync(user, MetricEntity);
    }

    public Task<OperationResult<bool>> KeyExistsAsync(string key)
    {
        return _redisCache.KeyExistsAsync(key, MetricEntity);
    }
}