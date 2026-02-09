using Microsoft.AspNetCore.Mvc.Razor;

namespace StoreProject.Infrastructure.Data
{
    public class FeatureViewLocationExpander : IViewLocationExpander
    {
        public void PopulateValues(ViewLocationExpanderContext context)
        {
            
        }

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            var controllerName = context.ActionContext.RouteData.Values["controller"]?.ToString();
            var featureName = GetFeatureName(context);

            return new[]
            {
                $"/Features/{featureName}/Views/{controllerName}/{{0}}.cshtml",
                $"/Features/{featureName}/Views/Shared/{{0}}.cshtml",
                "/Features/Shared/{0}.cshtml",
                "/Features/Shared/Components/{0}.cshtml",
                "/Views/Shared/{0}.cshtml",
            };
        }

        private string GetFeatureName(ViewLocationExpanderContext context)
        {
            var descriptor = context.ActionContext.ActionDescriptor;

            var ns = descriptor?.DisplayName;
            if (string.IsNullOrEmpty(ns))
                return "";

            var parts = ns.Split('.');
            var featureIndex = Array.IndexOf(parts, "Features");
            if (featureIndex >= 0 && parts.Length > featureIndex + 1)
                return parts[featureIndex + 1];

            return "";
        }
    }
}
