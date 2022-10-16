using ATI.Services.Common.Behaviors;
using ThousandWords.Core.Interfaces.DbContexts;
using ThousandWords.Core.Models;

namespace ThousandWords.Core.Services.GetWords;

public class LanguagePairsExtractor
{
    public int RequiredCount { get; set; }
    public int[] IdPairsForExclude { get; set; }
    public User User { get; set; }
    public LanguageDictionaryInfo DictionaryInfo { get; set; }

    private readonly ILanguagePairsDbContext _dbContext;

    public LanguagePairsExtractor(ILanguagePairsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<OperationResult<List<LanguagePair>>> ExtractAsync()
    {
        var keys = GetKeysRequiredPairs();
        return _dbContext.GetManyAsync(keys.ToList());
    }

    private IEnumerable<string> GetKeysRequiredPairs()
    {
        var idPairs = GetRequiredIdPairs();
        return idPairs.Select(id => $"{DictionaryInfo.Name}.{id}");
    }

    private IEnumerable<int> GetRequiredIdPairs()
    {
        var idPairs = new List<int>();
        for (int i = 0; i < DictionaryInfo.PairsCount && idPairs.Count < RequiredCount; i++)
        {
            if (IdPairsForExclude.Contains(i) == false && User.CompletedPairs.Contains(i) == false)
            {
                idPairs.Add(i);
            }
        }

        return idPairs;
    }
}