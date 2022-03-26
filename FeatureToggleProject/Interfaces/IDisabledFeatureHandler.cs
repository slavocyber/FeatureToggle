using Microsoft.AspNetCore.Mvc.Filters;

namespace FeaturesMaster.Interfaces
{
    public interface IDisabledFeatureHandler
    {
        Task HandleDisabledFeatures(IEnumerable<string> features, ActionExecutingContext context);
    }
}