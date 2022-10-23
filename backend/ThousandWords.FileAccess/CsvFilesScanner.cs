namespace ThousandWords.FileAccess;

public class CsvFilesScanner
{
    private const string SearchPatternFiles = "*.csv";

    public static List<string> GetFullnameFiles(string path)
    {
        try
        {
            return Directory.GetFiles(path, SearchPatternFiles).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return new List<string>();
        }
    }
}