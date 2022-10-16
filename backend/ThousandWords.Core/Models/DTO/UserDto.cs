using Newtonsoft.Json;

namespace ThousandWords.Core.Models.DTO;

public class UserDto
{
    [JsonProperty("email")]
    public string Email { get; set; }
    [JsonProperty("dictionary")]
    public string LanguageDictionaryName { get; set; }
}