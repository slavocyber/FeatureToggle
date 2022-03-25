using FeatureManager.Common.Models;

namespace FeatureManager.Interfaces;
public interface IBackgroundWorker : IDisposable
{
    List<Feature> Features { get; }
    void StopWorker();
}
