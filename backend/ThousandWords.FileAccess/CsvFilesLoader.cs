using ATI.Services.Common.Behaviors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using ThousandWords.Core.Extensions;
using ThousandWords.Core.Interfaces.ExternalLoaders;
using ThousandWords.Core.Models;

namespace ThousandWords.FileAccess;

public class CsvFilesLoader : ILanguageDictionariesLoader
{
    private readonly string _folderPath;
    
    private readonly CsvFilesScanner _filesScanner = new();
    private readonly CsvFileReader _csvFileReader = new();
    private readonly SimpleCsvParser _csvParser = new();

    public CsvFilesLoader(IOptions<FilesManagerOptions> options)
    {
        var folderPath = options.Value.DictionariesFolderPath;
        _folderPath = folderPath;
    }

    public async Task<OperationResult<IEnumerable<LanguageDictionary>>> LoadAsync()
    {
        var languageDictionaries = new List<LanguageDictionary>();
        var fullnameFiles = _filesScanner.GetFullnameFiles(_folderPath);
        foreach (var fullnameFile in fullnameFiles)
        {
            var fileName = Path.GetFileNameWithoutExtension(fullnameFile);
            var lines = await _csvFileReader.ReadAllLinesAsync(fullnameFile);
            var languagePairs = _csvParser.GetLanguagePairs(fileName, lines.ToArray().Randomize());
            languageDictionaries.Add(new LanguageDictionary
            {
                Name = fileName,
                Pairs = languagePairs
            });
        }

        return new OperationResult<IEnumerable<LanguageDictionary>>(languageDictionaries);
    }
}