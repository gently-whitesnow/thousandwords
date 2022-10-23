using ATI.Services.Common.Initializers.Interfaces;
using ThousandWords.Core.Interfaces.DbContexts;
using ThousandWords.Core.Interfaces.ExternalLoaders;
using ThousandWords.Core.Models;

namespace ThousandWords.Core.Initializers;

public class LanguageDictionariesInitializer : IInitializer
{
    private readonly ILanguageDictionariesLoader _loader;
    private readonly ILanguageDictionaryInfoDbContext _dictionaryInfoDbContext;
    private readonly ILanguagePairsDbContext _languagePairsDbContext;

    public LanguageDictionariesInitializer(
        ILanguageDictionariesLoader loader,
        ILanguageDictionaryInfoDbContext dictionaryInfoDbContext,
        ILanguagePairsDbContext languagePairsDbContext)
    {
        _loader = loader;
        _dictionaryInfoDbContext = dictionaryInfoDbContext;
        _languagePairsDbContext = languagePairsDbContext;
    }

    public async Task InitializeAsync()
    {
        var loadOperation = await _loader.LoadAsync();
        if (!loadOperation.Success)
        {
            Console.WriteLine(loadOperation.DumpAllErrors());
            return;
        }

        foreach (var languageDictionary in loadOperation.Value)
        {
            var dictionaryInfo = new LanguageDictionaryInfo
            {
                Name = languageDictionary.Name,
                PairsCount = languageDictionary.Pairs.Count()
            };
            var insertDictionaryInfoOperation = await _dictionaryInfoDbContext.InsertAsync(dictionaryInfo);
            if (!insertDictionaryInfoOperation.Success)
            {
                Console.WriteLine(insertDictionaryInfoOperation.DumpAllErrors());
                break;
            }

            var insertDictionaryPairsOperation = await _languagePairsDbContext.InsertManyAsync(languageDictionary.Pairs);
            if (!insertDictionaryPairsOperation.Success)
            {
                Console.WriteLine(insertDictionaryPairsOperation.DumpAllErrors());
                break;
            }
        }
    }

    public string InitStartConsoleMessage()
    {
        return "Start language dictionaries initializer";
    }

    public string InitEndConsoleMessage()
    {
        return "End language dictionaries initializer";
    }
}