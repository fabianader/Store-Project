using Microsoft.AspNetCore.Identity;

namespace StoreProject.Common
{
    public class CommonService
    {
        public static List<string> CheckErrors(IdentityResult result)
        {
            List<string> ErrorsList = new List<string>();
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ErrorsList.Add(error.Description);
            }

            return ErrorsList;
        }

        public static string GetContinueShoppingUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return "/";

            var firstSegment = url.TrimStart('/')
                .Split('/', StringSplitOptions.RemoveEmptyEntries)
                .FirstOrDefault()
                ?.ToLower();

            return firstSegment is "categories" or "products"
                ? url
                : "/";
        }

        public static string GetOrderStatusBootstrapColor(int statusIndex)
        {
            List<string> BootstrapColors = ["dark", "info", "primary", "success", "danger"];
            if(statusIndex < BootstrapColors.Count)
                return BootstrapColors[statusIndex];

            return "secondary";
        }
    }
}
