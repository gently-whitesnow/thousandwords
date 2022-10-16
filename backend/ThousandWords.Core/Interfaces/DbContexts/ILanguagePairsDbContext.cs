using ATI.Services.Common.Behaviors;
using ThousandWords.Core.Models;

namespace ThousandWords.Core.Interfaces.DbContexts;

public interface ILanguagePairsDbContext
{
    public Task<OperationResult<List<LanguagePair>>> GetManyAsync(List<string> keys);
    public Task<OperationResult> InsertManyAsync(IEnumerable<LanguagePair> languagePairs);
}