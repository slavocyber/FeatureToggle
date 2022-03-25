using Feature.Client.Common.Interfaces;
using Feature.Shared;
using System.Net.Http.Json;
using System.Text.Json;

namespace Feature.Client.Common;

internal class FeatureMaster : IFeatureMaster
{
    private const string GetJsonURL = "/get";
    private const string UpdateJsonURL = "/update";

    private readonly HttpClient _httpClient;

    private List<FeatureItem>? _features;

    public FeatureMaster(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<FeatureItem>> GetFeatures()
    {
        if (_features is null)
        {
            try
            {
                _features = await _httpClient.GetFromJsonAsync<List<FeatureItem>>(GetJsonURL);
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

        return _features!;
    }

    public ErrorCode Add(string newFeatName)
    {
        var newFeat = new FeatureItem() { Name = newFeatName };

        if (!Exist(newFeatName))
        {
            _features?.Add(newFeat);
            UpdateJson();

            return ErrorCode.JsonUpdated;
        }

        return ErrorCode.SameFeat;
    }

    public ErrorCode Remove(string featName)
    {
        foreach (var item in _features!)
        {
            if (item.Name == featName)
            {
                _ = _features?.Remove(item);
                UpdateJson();

                return ErrorCode.JsonUpdated;
            }
        }

        return ErrorCode.None;
    }

    public ErrorCode UpdateFeatureList(List<FeatureItem> newList)
    {
        _features = newList;
        UpdateJson();
        return ErrorCode.JsonUpdated;
    }

    public ErrorCode EditStatus(string featName)
    {
        foreach (var item in _features!)
        {
            if (item.Name == featName)
            {
                item.Status = !item.Status;
                UpdateJson();

                return ErrorCode.JsonUpdated;
            }
        }

        return ErrorCode.FeatIsNotFound;
    }

    private void UpdateJson()
    {
        try
        {
            _ = _httpClient.PostAsJsonAsync(UpdateJsonURL, _features);
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

    private bool Exist(string featName)
    {
        foreach (var item in _features!)
        {
            if (item.Name == featName)
            {
                return true;
            }
        }

        return false;
    }
}
