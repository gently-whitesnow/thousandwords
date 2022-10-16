using Newtonsoft.Json;

namespace ThousandWords.Core.Models.DTO;

public class LanguagePairDto
{
    [JsonProperty("word_id")]
    public int Id { get; set; }
    [JsonProperty("n_lang")]
    public string NativeWord { get; set; }
    [JsonProperty("f_lang")]
    public string ForeignWord { get; set; }
}