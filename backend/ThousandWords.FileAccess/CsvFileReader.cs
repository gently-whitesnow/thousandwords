using System.Text;

namespace ThousandWords.FileAccess;

public class CsvFileReader
{
    public async Task<IEnumerable<string>> ReadAllLinesAsync(string filePath)
    {
        try
        {
            var readLines = await TryReadAllLinesAsync(filePath);
            return readLines;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return new List<string>();
        }
    }

    private async Task<IEnumerable<string>> TryReadAllLinesAsync(string filePath)
    {
        return await File.ReadAllLinesAsync(filePath, Encoding.UTF8);
    }
}