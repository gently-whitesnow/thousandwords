namespace ThousandWords.Core.Models;

public class LanguageDictionary
{
    public string Name { get; set; }
    public IEnumerable<LanguagePair> Pairs { get; set; }
}