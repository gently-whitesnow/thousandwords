using ATI.Services.Common.Initializers;
using ATI.Services.Common.Initializers.Interfaces;
using JetBrains.Annotations;
using ThousandWords.Core.Interfaces.DbContexts;
using ThousandWords.Core.Interfaces.ExternalLoaders;
using ThousandWords.Core.Models;

namespace ThousandWords.Core.Initializers;

[UsedImplicitly]
[InitializeOrder(Order = InitializeOrder.Last)]
public class LanguageDictionariesInitializer : IInitializer
{
    private static bool _initialized;
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
        if (_initialized)
            return;

        var operation = await _loader.LoadAsync();
        if (operation.Success)
        {
            foreach (var languageDictionary in operation.Value)
            {
                var dictionaryInfo = new LanguageDictionaryInfo
                {
                    Name = languageDictionary.Name,
                    PairsCount = languageDictionary.Pairs.Count()
                };
                await _dictionaryInfoDbContext.InsertAsync(dictionaryInfo);
                await _languagePairsDbContext.InsertManyAsync(languageDictionary.Pairs);
            }

            _initialized = true;
        }
    }

    public string InitStartConsoleMessage()
    {
        return "Start language dictionaries initializer";
    }

    public string InitEndConsoleMessage()
    {
        return $"End language dictionaries initializer, result {_initialized}";
    }
}