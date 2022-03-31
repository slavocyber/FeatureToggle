using FeatureMaster.Interfaces;

namespace FeatureMaster;

public class Features : IFeatures
{
    private readonly string _jsonAllFeatures;
    private readonly Dictionary<string, bool> _listOfFeatures;

    public Features(string json, Dictionary<string, bool> listOfFeatures)
    {
        _jsonAllFeatures = json;
        _listOfFeatures = listOfFeatures;
    }

    public string AllFeatures()
    {
        var sr = new StreamReader(_jsonAllFeatures);
        var str = sr.ReadToEnd();
        sr.Close();
        return str;
    }

    public bool IsEnable(string nameOfFeature)
    {
        return _listOfFeatures.ContainsKey(nameOfFeature) && _listOfFeatures[nameOfFeature];
    }
}
