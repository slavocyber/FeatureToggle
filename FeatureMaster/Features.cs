using System.Text.Json;
using FeatureManager.Common.Models;
using FeatureManager.Interfaces;

namespace FeatureManager;

internal class Features : IFeatures
{
    private Dictionary<string, bool> _listOfFeatures;
    private Dictionary<string, bool> _listOfConfigs;
    private readonly string _cofigURL;
    private readonly IHttpMaster _httpMaster;

    public Features(string cofigURL, IHttpMaster httpMaster)
    {
        _cofigURL = cofigURL;
        _httpMaster = httpMaster;
    }

    public string AllFeatures() //delete me!!!
    {
        var sr = new StreamReader("Toggle.json");
        var str = sr.ReadToEnd();
        sr.Close();
        return str;
    }

    public bool IsEnable(string nameOfFeature)
    {
        if (_listOfFeatures is null)
            UpdateFeaturesData();

        return _listOfFeatures.ContainsKey(nameOfFeature) && _listOfFeatures[nameOfFeature];
    }

    /// <summary>
    /// Update list of features and configurations
    /// </summary>
    private void UpdateFeaturesData()
    {
        var jsonStr = _httpMaster.GetJsonData(_cofigURL).Result;

        var featSet = JsonSerializer.Deserialize<FeatureSettingModel>(jsonStr);

        _listOfFeatures = featSet.Features;
        _listOfConfigs = featSet.Configurations;
    }
}
