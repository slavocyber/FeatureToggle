using FeatureToggle.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FeatureToggle.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FeatureToggleController
    {
        private readonly ILogger<FeatureToggleController> _logger;
        private readonly IFeatureToggle _feature;

        public FeatureToggleController(ILogger<FeatureToggleController> logger, FeatureToggle feature)
        {
            _logger = logger;
            _feature = feature;
        }

        /// <summary>
        /// Get list of features in json
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet(Name = "features")]
        public string Get()
        {
            return _feature.EnableFeatures(); 
        }
    }
}
