using System.Text.Json.Serialization;

namespace Feature.Shared;

[Serializable]
public class FeatureItem
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("status")]
    public bool Status { get; set; }
}
