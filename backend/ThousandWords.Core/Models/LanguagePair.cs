using ATI.Services.Common.Caching.Redis;

namespace ThousandWords.Core.Models;

public class LanguagePair : ICacheEntity
{
    public string Key { get; set; }
    public int Id { get; set; }
    public string NativeWord { get; set; }
    public string ForeignWord { get; set; }
    
    public string GetKey()
    {
        return Key;
    }
}