using FeatureManager.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FeatureToggle.Controllers;

public class MainController : Controller
{
    private readonly IFeatures _toggle;

    public MainController(IFeatures toggle)
    {
        _toggle = toggle;
    }

    public IActionResult Index()
    {
        //var viewListOfFeat = Json(_toggle.IsEnable(""));

        //return View(viewListOfFeat);

        throw new NotImplementedException();
    }
}
