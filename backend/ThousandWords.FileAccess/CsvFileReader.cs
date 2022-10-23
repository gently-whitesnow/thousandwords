using System.Text;

namespace ThousandWords.FileAccess;

public class CsvFileReader
{
    public static async Task<IEnumerable<string>> ReadAllLinesAsync(string filePath)
    {
        try
        {
            return await File.ReadAllLinesAsync(filePath, Encoding.UTF8);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return new List<string>();
        }
    }
}