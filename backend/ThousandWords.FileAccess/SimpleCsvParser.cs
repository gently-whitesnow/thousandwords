using ThousandWords.Core.Models;

namespace ThousandWords.FileAccess;

public class SimpleCsvParser // line parsing form: {native_word},{foreign_word}
{
    private const char LanguagePairCsvSeparator = ',';

    public LanguagePair[] GetLanguagePairs(string suffixKey, string[] fileLines)
    {
        var pairs = new List<LanguagePair>();
        
        for (int i = 0; i < fileLines.Length; i++)
        {
            var line = fileLines[i];
            var words = line.Trim().Split(LanguagePairCsvSeparator);
            if (words.Length >= 2)
            {
                var pair = new LanguagePair
                {
                    Key = $"{suffixKey}.{i}",
                    Id = i,
                    NativeWord = words[0],
                    ForeignWord = words[1]
                };
                pairs.Add(pair);
            }
        }

        return pairs.ToArray();
    }
}