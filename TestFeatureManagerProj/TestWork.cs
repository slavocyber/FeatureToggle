﻿using FeatureManager.Interfaces;

namespace TestFeatureManagerProj;

public class TestWork : ITestWork
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
            Thread.Sleep(500);
            
            foreach (var feature in _featuresName)
            {
                Console.WriteLine(_featManager.IsEnable(feature)
                    ? $"{feature} is enable"
                    : $"{feature} is disable");
            }
        }
    }
}

public interface ITestWork
{
    void Start();
}