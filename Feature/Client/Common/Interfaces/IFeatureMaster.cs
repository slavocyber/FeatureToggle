using Feature.Shared;

namespace Feature.Client.Common.Interfaces;

internal interface IFeatureMaster
{
    Task<List<FeatureItem>> GetFeatures();
    ErrorCode Add(string newFeatName);
    ErrorCode Remove(string featName);
    ErrorCode UpdateFeatureList(List<FeatureItem> newList);
    ErrorCode EditStatus(string featName);
}
