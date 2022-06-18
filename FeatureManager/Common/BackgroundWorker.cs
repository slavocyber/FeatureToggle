using System.Net.Http.Json;
using System.Text.Json;
using FeatureManager.Common.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FeatureManager.Common;
public class BackgroundWorker : BackgroundService
{
    public List<Feature>? Features { get; private set; }
    
    private readonly HttpClient _httpClient;
    private readonly ILogger<BackgroundWorker>? _logger;
    private readonly IOptionsSnapshot<SettingsUpdate>? _options;
    
    private string? _getJsonUrl;
    private int _intervalUpdate;

    public BackgroundWorker(ILogger<BackgroundWorker> logger, IOptionsSnapshot<SettingsUpdate>? options)
    {
        _httpClient = new HttpClient();

        _logger = logger;
        _options = options;
    }

    public override void Dispose()
    {
        _logger.LogInformation($"BackgroundWorker is disposing");

        base.Dispose();
        _httpClient.Dispose();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("BackgroundWorker is staring");
        stoppingToken.Register(() => _logger.LogInformation("BackgroundWorker is stopping"));

        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation($"BackgroundWorker doing work");

            GetSettingUpdate();
            await DoWorkAsync();

            await Task.Delay(_intervalUpdate, stoppingToken);
        }

        _logger.LogInformation($"BackgroundWorker is stopping");
    }
    
    private async Task DoWorkAsync()
    {
        if (string.IsNullOrEmpty(_getJsonUrl))
        {
            _logger.LogError("Get json url is null or empty.");

            Features = null;
            return;
        }
        
        try
        {
            Features = await _httpClient.GetFromJsonAsync<List<Feature>>(_getJsonUrl)
                ?? throw new ArgumentNullException("Get list of features from server is null");
        }
        catch (HttpRequestException) // Non success
        {
            _logger.LogError("An error occurred.");

            Features = null;
        }
        catch (NotSupportedException) // When content type is not valid
        {
            _logger.LogError("The content type is not supported.");

            Features = null;
        }
        catch (JsonException) // Invalid JSON
        {
            _logger.LogError("Invalid JSON.");

            Features = null;
        }
        catch (ArgumentNullException) // Recived list of features from server is null
        {
            _logger.LogError("Invalid JSON.");

            Features = null;
        }
        catch (Exception e) //unhandle exep
        {
            _logger.LogError(e, e.Message);

            Features = null;
        }
    }

    private void GetSettingUpdate()
    {
        if (_options is null)
        {
            _getJsonUrl = string.Empty;
            _intervalUpdate = 5_000;
            return;
        }

        _logger.LogDebug("BackgroundWorker is updating setting");

        _getJsonUrl = _options.Value.UrlUpdate;
        _intervalUpdate = _options.Value.IntervalUpdate;
    }
}
