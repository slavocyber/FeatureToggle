using System.Net.Http.Json;
using System.Text.Json;
using FeatureManager.Common.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FeatureManager.Common;
public class BackgroundWorker : BackgroundService
{
    public static List<Feature>? Features { get; private set; }
    
    private readonly HttpClient _httpClient;
    private readonly ILogger<BackgroundWorker>? _logger;
    
    private string _getJsonUrl;
    private int _intervalUpdate;

    public BackgroundWorker(ILogger<BackgroundWorker> logger, IOptions<SettingsUpdate> options)
    {
        _ = logger ?? throw new ArgumentNullException(nameof(logger));
        _ = options ?? throw new ArgumentNullException(nameof(options));
        _ = options.Value.UrlUpdate ?? throw new ArgumentNullException(nameof(options.Value.IntervalUpdate));
        
        _httpClient = new HttpClient();

        _logger = logger;
        _getJsonUrl = "https://localhost:7246/get";
        _intervalUpdate = 5_000;
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

            await GetFeaturesAsync();

            await Task.Delay(_intervalUpdate, stoppingToken);
        }

        _logger.LogInformation($"BackgroundWorker is stopping");
    }
    
    private async Task GetFeaturesAsync()
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

            Console.WriteLine($"{Features[0].Name}: {Features[0].Status}");
        }
        catch (HttpRequestException) // Non success
        {
            _logger.LogError("An error occurred.");

            _intervalUpdate = 5_000;
            Features = null;
        }
        catch (NotSupportedException) // When content type is not valid
        {
            _logger.LogError("The content type is not supported.");

            _intervalUpdate = 5_000;
            Features = null;
        }
        catch (JsonException) // Invalid JSON
        {
            _logger.LogError("Invalid JSON.");

            _intervalUpdate = 5_000;
            Features = null;
        }
        catch (ArgumentNullException) // Recived list of features from server is null
        {
            _logger.LogError("Invalid JSON.");

            _intervalUpdate = 5_000;
            Features = null;
        }
        catch (Exception e) //unhandle exep
        {
            _logger.LogError(e, e.Message);

            _intervalUpdate = 5_000;
            Features = null;
        }
    }
}
