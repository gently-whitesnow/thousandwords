namespace ThousandWords.FileAccess;

public class CsvFilesScanner
{
    private const string SearchPatternFiles = "*.csv";

    public IEnumerable<string> GetFullnameFiles(string path)
    {
        try
        {
            var csvFiles = TryGetFiles(path);
            return csvFiles;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return new List<string>();
        }
    }

    private IEnumerable<string> TryGetFiles(string path)
    {
        return Directory.GetFiles(path, SearchPatternFiles);
    }
}