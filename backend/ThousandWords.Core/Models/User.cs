using ATI.Services.Common.Caching.Redis;
using ThousandWords.Core.Utils;

namespace ThousandWords.Core.Models;

public class User : ICacheEntity
{
    public string Key { get; set; }
    public string LanguageDictionaryName { get; set; }
    public HashSet<int> CompletedPairs { get; set; }
    
    public string GetKey()
    {
        return Key;
    }

    public static string GetKey(string dictionary, string email)
    {
        return $"{dictionary}.{Sha256Converter.Convert(email)}";
    }
}