using FeatureToggle.Interfaces;

namespace FeatureToggle
{
    public class FeatureToggle : IFeatureToggle
    {
        private readonly ILogger<FeatureToggle> _logger;
        private readonly string _jsonListOfFeatures;
        private Dictionary<string, bool> listOfFeatures; //???


        public FeatureToggle(ILogger<FeatureToggle> logger, string jsonListOfFeatures)
        {
            _logger = logger;
            _jsonListOfFeatures = jsonListOfFeatures;
            listOfFeatures = new Dictionary<string, bool>();
        }

        public bool IsEnable(string featureName)
        {
            if (listOfFeatures.Count == 0)
            {
                UpdateListOfFeatures();
            }

            try
            {
                return listOfFeatures[featureName];
            }
            catch (Exception)
            {

                return false;
            }
        }

        /// <summary>
        /// create dic. list of features from json
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void UpdateListOfFeatures()
        {
            throw new NotImplementedException();
        }
    }
}
