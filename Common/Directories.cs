namespace StoreProject.Common
{
    public class Directories
    {
        public const string DefaultUserProfilePicture = "wwwroot/images/user";
        public static string GetUserProfilePicture(string ProfilePicture) => $"{DefaultUserProfilePicture.Replace("wwwroot", "")}/{ProfilePicture}";

    }
}
