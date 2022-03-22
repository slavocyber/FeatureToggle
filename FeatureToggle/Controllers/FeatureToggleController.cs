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

        public FeatureToggleController(ILogger<FeatureToggleController> logger, IFeatureToggle feature)
        {
            _logger = logger;
            _feature = feature;
        }

        [HttpGet(Name = "features")]
        public string Get()
        {
            if (_feature.IsEnable("fetureOne"))
            {
                throw new NotImplementedException();
            }

            throw new NotImplementedException();
        }
    }
}
