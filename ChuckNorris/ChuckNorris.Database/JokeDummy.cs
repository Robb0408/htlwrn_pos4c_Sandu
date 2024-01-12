using System.Text.Json.Serialization;

namespace ChuckNorris.Database;

public class JokeDummy
{
    [JsonPropertyName("categories")]
    public string[] Categories { get; set; } = [];
    [JsonPropertyName("created_at")] 
    public string CreatedAt { get; set; } = null!;
    [JsonPropertyName("icon_url")]
    public string IconUrl { get; set; } = null!;
    [JsonPropertyName("id")]
    public string Id { get; set; } = null!;
    [JsonPropertyName("updated_at")]
    public string UpdatedAt { get; set; } = null!;
    [JsonPropertyName("url")]
    public string Url { get; set; } = null!;
    [JsonPropertyName("value")]
    public string Value { get; set; } = null!;
}