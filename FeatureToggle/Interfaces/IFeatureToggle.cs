namespace FeatureToggle.Interfaces
{
    public interface IFeatureToggle
    {
        public bool IsEnable(string featureName);
    }
}
