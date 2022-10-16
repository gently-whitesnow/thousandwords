using ATI.Services.Common.Behaviors;
using ThousandWords.Core.Models;

namespace ThousandWords.Core.Interfaces.DbContexts;

public interface ILanguageDictionaryInfoDbContext
{
    public Task<OperationResult<LanguageDictionaryInfo>> GetLanguageDictionaryByNameAsync(string name);
    public Task<OperationResult> InsertAsync(LanguageDictionaryInfo info);
}