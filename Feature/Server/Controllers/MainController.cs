using Feature.Server.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Feature.Server.Controllers;

[ApiController]
[Route("/get")]
public class MainController : ControllerBase
{
    private readonly ILogger<MainController> _logger;
    private readonly IJsonMaster _jsonMaster;

    public MainController(IJsonMaster jsonMaster, ILogger<MainController> logger)
    {
        _logger = logger;
        _jsonMaster = jsonMaster;
    }

    [HttpGet]
    public async Task<string> Get()
    {
        _logger.LogInformation("Recive /get call");

        return await _jsonMaster.ReadAsync();
    }
}
