using FeatureManager.Common.Models;

namespace FeatureManager.Interfaces;
public interface IBackgroundWorker
{
    List<Feature> Features { get; }
}
