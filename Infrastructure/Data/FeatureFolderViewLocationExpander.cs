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
            // مثلا Controller: HomeController داخل Features/Home/Controllers
            var controllerName = context.ActionContext.RouteData.Values["controller"]?.ToString();
            var featureName = GetFeatureName(context);

            return new[]
            {
                $"/Features/{featureName}/Views/{controllerName}/{{0}}.cshtml",   // مسیر مورد نظر تو
                $"/Features/{featureName}/Views/Shared/{{0}}.cshtml",             // shared viewهای محلی هر feature
                "/Features/Shared/{0}.cshtml",                                    // shared viewهای کل سیستم
                "/Views/Shared/{0}.cshtml",                                        // مسیر کلاسیک MVC
            };
        }

        private string GetFeatureName(ViewLocationExpanderContext context)
        {
            var descriptor = context.ActionContext.ActionDescriptor;

            // مثلاً: Namespace = ProjectName.Features.Home.Controllers.HomeController
            var ns = descriptor?.DisplayName;
            if (string.IsNullOrEmpty(ns))
                return "";

            var parts = ns.Split('.');
            var featureIndex = Array.IndexOf(parts, "Features");
            if (featureIndex >= 0 && parts.Length > featureIndex + 1)
                return parts[featureIndex + 1]; // مثلاً Home

            return "";
        }
    }

}
