namespace FeatureManager.Interfaces;

internal interface IHttpMaster
{
    void Dispose();
    Task<string> GetJsonData(string URL);
}
