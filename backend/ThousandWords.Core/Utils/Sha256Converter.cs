using System.Security.Cryptography;
using System.Text;

namespace ThousandWords.Core.Utils;

public static class Sha256Converter
{
    public static string Convert(string inputString)
    {
        using HashAlgorithm hashAlgorithm = SHA256.Create();
        var encodedBytes = Encoding.UTF8.GetBytes(inputString);
        var hashBytes = hashAlgorithm.ComputeHash(encodedBytes);
        return System.Convert.ToBase64String(hashBytes);
    }
}