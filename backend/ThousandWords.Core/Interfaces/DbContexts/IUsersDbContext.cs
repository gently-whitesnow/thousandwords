using ATI.Services.Common.Behaviors;
using ThousandWords.Core.Models;

namespace ThousandWords.Core.Interfaces.DbContexts;

public interface IUsersDbContext
{
    public Task<OperationResult<User>> GetUserByKeyAsync(string key);
    public Task<OperationResult> InsertAsync(User user);
    public Task<OperationResult<bool>> KeyExistsAsync(string key);
}