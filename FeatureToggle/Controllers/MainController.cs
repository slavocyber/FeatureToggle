using Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FeatureToggle.Controllers
{
    public class MainController : Controller
    {
        private readonly IFeatures _toggle;

        public MainController(IFeatures toggle)
        {
            _toggle = toggle;
        }

        public IActionResult Index()
        {
            var zxc = Json(_toggle.GetData());

            return View(zxc);
        }
    }
}
