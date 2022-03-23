using FeatureToggle.Interfaces;

namespace FeatureToggle
{
    public class FeatureToggle : IFeatureToggle
    {
        private readonly ILogger<FeatureToggle> _logger;

        public FeatureToggle(ILogger<FeatureToggle> logger)
        {
            _logger = logger;
        }

        public string EnableFeatures()
        {
            throw new NotImplementedException();
        }
    }
}
