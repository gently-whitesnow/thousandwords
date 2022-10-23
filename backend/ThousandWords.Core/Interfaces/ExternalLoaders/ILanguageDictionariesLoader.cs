using ATI.Services.Common.Behaviors;
using ThousandWords.Core.Models;

namespace ThousandWords.Core.Interfaces.ExternalLoaders;

public interface ILanguageDictionariesLoader
{
    public Task<OperationResult<List<LanguageDictionary>>> LoadAsync();
}