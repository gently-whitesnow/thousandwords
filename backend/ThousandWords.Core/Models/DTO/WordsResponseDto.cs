using Newtonsoft.Json;

namespace ThousandWords.Core.Models.DTO;

public class WordsResponseDto
{
    [JsonProperty("user_level")]
    public int UserLevel { get; set; }
    [JsonProperty("words")]
    public IEnumerable<LanguagePairDto> Words { get; set; }
}