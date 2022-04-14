using System.Net.Http.Json;
using System.Text.Json;

namespace AdminFeature.Pages;

public partial class AdminPanel
{
    private const string URL = "FeatureToggle/features.json";

    private readonly string _newFeature = string.Empty;

    private bool _invalidInput;
    private Dictionary<string, bool>? _features;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            //loocking for in the "wwwroot" folder
            _features = await Http.GetFromJsonAsync<Dictionary<string, bool>>(URL)
            ?? new Dictionary<string, bool>();
        }
        catch (HttpRequestException) //non success
        {
            //logic..

            throw new HttpRequestException();
        }
        catch (NotSupportedException) //when conect type is not valid
        {
            //logic..

            throw new NotSupportedException();
        }
        catch (JsonException) //invalid json
        {
            //logic..

            throw new JsonException();
        }
    }

    private void OnOffButton(string key)
    {
        _features![key] = !_features[key];

        _ = UpdateJson();
    }

    private void Add(string featName)
    {
        if (_features!.ContainsKey(featName))
        {
            throw new InvalidOperationException("This feature name is invalid");
        }

        _features.Add(featName, false);

        _ = UpdateJson();
    }

    private void Delete(string featName)
    {
        _ = _features!.Remove(featName);

        _ = UpdateJson();
    }

    private async Task UpdateJson()
    {
        _invalidInput = false;

        //sent http request to server
    }
}
