using Feature.Server.Interfaces;
using Feature.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Feature.Server.Controllers;

[ApiController]
[Route("/update")]
public class UpdateController : ControllerBase
{
    private readonly IJsonMaster _jsonMaster;
    private readonly ILogger<MainController> _logger;

    public UpdateController(IJsonMaster jsonMaster, ILogger<MainController> logger)
    {
        _jsonMaster = jsonMaster;
        _logger = logger;
    }

    [HttpPost]
    public async Task PostUpdateListOfFeatures([FromBody] List<FeatureItem> list)
    {
        _logger.LogInformation("/update open, with params: ", list);

        await _jsonMaster.WriteAsync(list);
    }
}
