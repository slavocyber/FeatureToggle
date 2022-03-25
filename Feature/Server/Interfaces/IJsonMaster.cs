using Feature.Shared;

namespace Feature.Server.Interfaces;

public interface IJsonMaster
{
    Task<string> ReadAsync();
    Task WriteAsync(List<FeatureItem> newList);
}

