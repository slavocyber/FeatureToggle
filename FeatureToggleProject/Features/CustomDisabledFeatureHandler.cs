using FeaturesMaster.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FeaturesMaster.Common
{
    public class CustomDisabledFeatureHandler : IDisabledFeatureHandler
    {
        public Task HandleDisabledFeatures(IEnumerable<string> features, ActionExecutingContext context)
        {
            context.Result = new ContentResult
            {
                ContentType = "text/plain",
                Content = "This feature is not available please try again later - " + string.Join(',', features),
                StatusCode = 404
            };

            return Task.CompletedTask;
        }
    }
}
