﻿using FeatureManager.Interfaces;

namespace FeatureManager;

public class FeatManager : IFeatureManager
{
    private readonly IBackgroundWorker _backgroundWorker;

    public FeatManager(IBackgroundWorker backgroundWorker)
    {
        _backgroundWorker = backgroundWorker;
    }

    /// <summary>
    /// Looks for input name of feature in list of features in BackgroundWorker
    /// </summary>
    /// <param name="nameOfFeature">feature name</param>
    /// <returns>feat status</returns>
    /// <exception cref="NullReferenceException"></exception>
    public bool IsEnable(string nameOfFeature)
    {
        return string.IsNullOrEmpty(nameOfFeature)
            ? throw new InvalidDataException($"{nameof(nameOfFeature)}: is {nameOfFeature}")
            : _backgroundWorker.Features.FirstOrDefault(f => f.Name!.Equals(nameOfFeature, StringComparison.Ordinal))?.Status ?? false;
    }
}
