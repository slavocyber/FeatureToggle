namespace FeatureManager.Common.Models;

public class SettingsUpdate
{
    public const string Position = "UpdateSettings";

    public string? UrlUpdate { get; set; } = string.Empty;
    public int IntervalUpdate { get; set; }
}
