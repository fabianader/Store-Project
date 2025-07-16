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
    }
}
