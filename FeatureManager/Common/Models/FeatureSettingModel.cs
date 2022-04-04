namespace FeatureManager.Common.Models;

[Serializable]
internal class FeatureSettingModel
{
    public Dictionary<string, bool> Features;
    public Dictionary<string, bool> Configurations;
}
