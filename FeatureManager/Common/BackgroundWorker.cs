using System.Net.Http.Json;
using System.Text.Json;
using FeatureManager.Common.Models;
using FeatureManager.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace FeatureManager.Common;
public class BackgroundWorker : BackgroundService, IBackgroundWorker
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<BackgroundWorker>? _logger;
    private readonly string _getJsonURL;
    private readonly int _timeUpdate;

    public List<Feature>? Features { get; private set; }

    public BackgroundWorker(IServiceProvider serviceProvider, string getJsonUrl, int timeUpdate)
    {
        _httpClient = new HttpClient();

        _logger = serviceProvider.GetService<ILogger<BackgroundWorker>>() ??
            NullLogger<BackgroundWorker>.Instance;
        _getJsonURL = getJsonUrl;
        _timeUpdate = timeUpdate;
    }

    private async Task DoWorkAsync()
    {
        try
        {
            Features = await _httpClient.GetFromJsonAsync<List<Feature>>(_getJsonURL)
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

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation($"BackgroundWorker is staring");

        stoppingToken.Register(() => _logger.LogInformation($"BackgroundWorker is stopping"));

        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation($"BackgroundWorker doing work");

            await DoWorkAsync();

            await Task.Delay(_timeUpdate, stoppingToken);
        }

        _logger.LogInformation($"BackgroundWorker is stopping");
    }

    public override void Dispose()
    {
        _logger.LogInformation($"BackgroundWorker is disposing");

        base.Dispose();
        _httpClient.Dispose();
    }
}
