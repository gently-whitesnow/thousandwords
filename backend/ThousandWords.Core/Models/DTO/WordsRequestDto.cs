using Newtonsoft.Json;

namespace ThousandWords.Core.Models.DTO;

public class WordsRequestDto
{
    [JsonProperty("word_id")]
    public int CompletedWordId { get; set; }
    [JsonProperty("count")]
    public int RequiredWordsCount { get; set; }
    [JsonProperty("queue_words")]
    public List<int> SessionWordsId { get; set; }
}