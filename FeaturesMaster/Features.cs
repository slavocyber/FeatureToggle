using FeaturesMaster.Interfaces;

namespace FeaturesMaster;

public class Features : IFeatures
{
    private readonly string _jsonAllFeatures;

    public Features(string json)
    {
        _jsonAllFeatures = json;
    }

    public string EnableFeatures()
    {
        var sr = new StreamReader(_jsonAllFeatures);
        var str = sr.ReadToEnd();
        sr.Close();
        return str;
    }
}
