using System.Net.Http.Json;
using System.Text.Json;
using FeatureMaster.Interfaces;

namespace FeatureMaster.Common;
internal class HttpMaster : IDisposable, IHttpMaster
{
    private readonly HttpClient _httpClient;

    public HttpMaster()
    {
        _httpClient = new HttpClient();
    }

    public async Task<string> GetJsonData(string URL)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<string>(URL);
        }
        catch (HttpRequestException) //non success
        {
            throw new HttpRequestException();
        }
        catch (NotSupportedException) //when conect type is not valid
        {
            throw new NotSupportedException();
        }
        catch (JsonException) //invalid json
        {
            throw new JsonException();
        }
    }

    public void Dispose()
    {
        _httpClient?.Dispose();
    }
}
