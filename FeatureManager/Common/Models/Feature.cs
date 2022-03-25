using System.Text.Json.Serialization;

namespace FeatureManager.Common.Models;

[Serializable]
public class Feature
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("status")]
    public bool Status { get; set; }
}
