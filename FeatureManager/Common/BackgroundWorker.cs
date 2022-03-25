using System.Net.Http.Json;
using System.Text.Json;
using FeatureManager.Common.Models;
using FeatureManager.Interfaces;

namespace FeatureManager.Common;
public class BackgroundWorker : IBackgroundWorker
{
    private readonly HttpClient _httpClient;
    private readonly System.Timers.Timer _timer;
    private readonly string _getJsonURL;

    public List<Feature> Features { get; private set; }

    public BackgroundWorker(HttpClient httpClient, string getJsonUrl, int timeUpdate)
    {
        Features = new List<Feature>();

        _getJsonURL = getJsonUrl;
        _httpClient = httpClient;

        _ = DoWorkAsync();

        //Create a timer with an interval.
        _timer = new System.Timers.Timer(timeUpdate);

        // Hook up the Elapsed event for the timer. 
        _timer.Elapsed += async (sender, e) => await DoWorkAsync();
        _timer.AutoReset = true;
        _timer.Enabled = true;
        _timer.Start();
    }

    public void Dispose()
    {
        StopWorker();

        _timer.Dispose();
        _httpClient.Dispose();
    }

    public void StopWorker()
    {
        _timer.Stop();
    }

    private async Task DoWorkAsync()
    {
        try
        {
            Features = await _httpClient.GetFromJsonAsync<List<Feature>>(_getJsonURL) ?? new List<Feature>();
        }
        catch (HttpRequestException) // Non success
        {
            //Console.WriteLine("An error occurred.");
        }
        catch (NotSupportedException) // When content type is not valid
        {
            //Console.WriteLine("The content type is not supported.");
        }
        catch (JsonException) // Invalid JSON
        {
            //Console.WriteLine("Invalid JSON.");
        }
        catch (Exception e) //unhandle exep
        {
            //Console.WriteLine(e);
        }
    }
}
