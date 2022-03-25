using Interfaces;

namespace testzxc
{
    public class FeatureToggle : IFeatureToggle
    {
        private readonly string _nameJSON;

        public FeatureToggle(string json)
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
