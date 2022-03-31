using FeatureMaster.Interfaces;

namespace FeatureMaster;

internal class Features : IFeatures
{
    private readonly Dictionary<string, bool> _listOfFeatures;
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
        {

        }

        return _listOfFeatures.ContainsKey(nameOfFeature) && _listOfFeatures[nameOfFeature];
    }
}
