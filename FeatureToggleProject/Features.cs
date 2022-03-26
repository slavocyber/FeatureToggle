using Common;
using Interfaces;

namespace FeaturesMaster
{
    public class Features : IFeatures
    {
        private readonly string _jsonAllFeatures;

        public Features(string json)
        {
            _jsonAllFeatures = json;
        }

        public Dictionary<string, bool> EnableFeatures()
        {
            throw new NotImplementedException();
        }

        public string GetData()
        {
            var sr = new StreamReader(_jsonAllFeatures);
            var str = sr.ReadToEnd();
            sr.Close();
            return str;
        }
    }
}
