using ATI.Services.Common.Behaviors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using ThousandWords.Core.Extensions;
using ThousandWords.Core.Interfaces.ExternalLoaders;
using ThousandWords.Core.Models;

namespace ThousandWords.FileAccess;

public class CsvFilesLoader : ILanguageDictionariesLoader
{
    private readonly FilesManagerOptions _options;
    public CsvFilesLoader(IOptions<FilesManagerOptions> options)
    {
        _options = options.Value;
    }

    public async Task<OperationResult<List<LanguageDictionary>>> LoadAsync()
    {
        var languageDictionaries = new List<LanguageDictionary>();
        var fullnameFiles = CsvFilesScanner.GetFullnameFiles(_options.DictionariesFolderPath);
        foreach (var fullnameFile in fullnameFiles)
        {
            var fileName = Path.GetFileNameWithoutExtension(fullnameFile);
            var lines = await CsvFileReader.ReadAllLinesAsync(fullnameFile);
            
            var getLanguagePairsOperation = SimpleCsvParser.GetLanguagePairs(fileName, lines.ToArray().Randomize());
            if (!getLanguagePairsOperation.Success)
                return new OperationResult<List<LanguageDictionary>>(getLanguagePairsOperation);
            
            languageDictionaries.Add(new LanguageDictionary
            {
                Name = fileName,
                Pairs = getLanguagePairsOperation.Value
            });
        }

        return new OperationResult<List<LanguageDictionary>>(languageDictionaries);
    }
}