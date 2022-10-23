using ATI.Services.Common.Behaviors;
using ThousandWords.Core.Models;

namespace ThousandWords.FileAccess;

public class SimpleCsvParser // line parsing form: {native_word},{foreign_word}
{
    private const char LanguagePairCsvSeparator = ',';

    public static OperationResult<List<LanguagePair>> GetLanguagePairs(string suffixKey, string[] fileLines)
    {
        var pairs = new List<LanguagePair>();

        for (var i = 0; i < fileLines.Length; i++)
        {
            var line = fileLines[i];
            var words = line.Trim().Split(LanguagePairCsvSeparator);
            if (words.Length != 2)
                return new OperationResult<List<LanguagePair>>(ActionStatus.BadRequest,
                    $"Неправильно сформирован excel файл, ошибка: {line}", "excel_error");

            pairs.Add(new LanguagePair
            {
                Key = $"{suffixKey}.{i}",
                Id = i,
                NativeWord = words[0],
                ForeignWord = words[1]
            });
        }

        return new OperationResult<List<LanguagePair>>(pairs.ToList());
    }
}