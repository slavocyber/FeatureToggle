namespace FeatureManager.Common.Models;

public class SettingsUpdate
{
    public const string BackgroundWorkerSettings = "BackgroundWorkerSettings";

    public string? UrlUpdate { get; set; }
    public int IntervalUpdate { get; set; }

}