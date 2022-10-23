using Newtonsoft.Json;
using ThousandWords.Core.Models.DTO;

namespace ThousandWords.Core.Models;

public class WordsResponse
{
    [JsonProperty("user_level")]
    public int UserLevel { get; set; }
    [JsonProperty("words")]
    public IEnumerable<LanguagePairDto> Words { get; set; }
}