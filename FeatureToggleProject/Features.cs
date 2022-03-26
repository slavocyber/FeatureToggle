using Common;
using Interfaces;

namespace FeaturesMaster
{
    public class Features : IFeatures
    {
        private readonly string _nameJSON;
        private readonly FeatConf _config;

        public Features(string json, FeatConf config)
        {
            _nameJSON = json;
            _config = config;
        }

        public string EnableFeatures()
        {
            throw new NotImplementedException();
        }

        public string GetData()
        {
            var sr = new StreamReader(_nameJSON);
            var str = sr.ReadToEnd();
            sr.Close();
            return str;
        }
    }
}
