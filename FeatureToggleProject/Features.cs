using Interfaces;

namespace FeaturesMaster
{
    public class Features : IFeatures
    {
        private readonly string _nameJSON;

        public Features(string json)
        {
            _nameJSON = json;
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
