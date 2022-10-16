using ATI.Services.Common.Caching.Redis;

namespace ThousandWords.Core.Models;

public class LanguageDictionaryInfo : ICacheEntity
{
    public string Name { get; set; }
    public int PairsCount { get; set; }

    public string GetKey() => Name;
}