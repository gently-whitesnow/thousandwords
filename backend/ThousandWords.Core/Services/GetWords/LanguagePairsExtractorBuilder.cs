using ThousandWords.Core.Interfaces.DbContexts;
using ThousandWords.Core.Models;

namespace ThousandWords.Core.Services.GetWords;

public class LanguagePairsExtractorBuilder
{
    private readonly LanguagePairsExtractor _pairsExtractor;

    private LanguagePairsExtractorBuilder(ILanguagePairsDbContext dbContext)
    {
        _pairsExtractor = new LanguagePairsExtractor(dbContext);
    }

    public static LanguagePairsExtractorBuilder FromDbContext(ILanguagePairsDbContext dbContext)
    {
        return new LanguagePairsExtractorBuilder(dbContext);
    }

    public LanguagePairsExtractorBuilder ForUser(User user)
    {
        _pairsExtractor.User = user;
        return this;
    }

    public LanguagePairsExtractorBuilder WithExcludes(IEnumerable<int> excludeIds)
    {
        _pairsExtractor.IdPairsForExclude = excludeIds.ToArray();
        return this;
    }

    public LanguagePairsExtractorBuilder SetLanguageDictionaryInfo(LanguageDictionaryInfo dictionaryInfo)
    {
        _pairsExtractor.DictionaryInfo = dictionaryInfo;
        return this;
    }

    public LanguagePairsExtractorBuilder SetRequiredCount(int count)
    {
        _pairsExtractor.RequiredCount = count;
        return this;
    }

    public LanguagePairsExtractor Build()
    {
        return _pairsExtractor;
    }
}