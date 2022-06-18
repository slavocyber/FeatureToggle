using FeatureManager.Interfaces;

namespace TestFeatureManagerProj;

public class TestWork
{
    private readonly IFeatManager _featManager;

    private readonly List<string> _featuresName = new()
    {
        "feature_one",
        "feature_1",
        "feature1"
    };

    public TestWork(IFeatManager featManager)
    {
        _featManager = featManager;
    }

    public void Start()
    {
        while (true)
        {
            foreach (var feature in _featuresName)
            {
                Console.WriteLine(_featManager.IsEnable(feature) 
                    ? $"{feature} is enable" 
                    : $"{feature} is disable");
            }
            
            Thread.Sleep(500);
        }
    }
}
