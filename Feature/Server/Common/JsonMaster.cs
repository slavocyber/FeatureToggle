using System.Text.Json;
using Feature.Server.Interfaces;
using Feature.Shared;

namespace Feature.Server.Common;

public class JsonMaster : IJsonMaster
{
    private const string Path = "features.json";

    private readonly ILogger<JsonMaster> _logger;

    public JsonMaster(ILogger<JsonMaster> logger)
    {
        _logger = logger;

        if (!File.Exists(Path))
        {
            _ = File.Create(Path);
        }
    }

    public async Task<string> ReadAsync()
    {
        _logger.LogInformation("Call JsonMaster.ReadAsync");

        using var sourceStream = new FileStream(Path, FileMode.OpenOrCreate);
        using var reader = new StreamReader(sourceStream);

        return await reader.ReadToEndAsync();
    }

    public Task WriteAsync(List<FeatureItem> newList)
    {
        _logger.LogInformation("Call JsonMaster.WriteAsync");

        var json = JsonSerializer.Serialize(newList);

        _logger.LogInformation("Serialized to json ", json);

        File.WriteAllText(Path, json);

        return Task.CompletedTask;
    }
}

