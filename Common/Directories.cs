namespace StoreProject.Common
{
    public class Directories
    {
        public const string UserProfilePicture = "wwwroot/images/users";
        public const string ProductImage = "wwwroot/images/products";
        public const string IconImage = "wwwroot/images/icons";
        public const string BannerImage = "wwwroot/images/banners";
        public static string GetUserProfilePicture(string ProfilePicture) => $"{UserProfilePicture.Replace("wwwroot", "")}/{ProfilePicture}";
        public static string GetProductImage(string ProductImageUrl) => $"{ProductImage.Replace("wwwroot", "")}/{ProductImageUrl}";
        public static string GetIconImage(string IconImageUrl) => $"{IconImage.Replace("wwwroot", "")}/{IconImageUrl}";
        public static string GetBannerImage(int BannerImageNumber) => $"{BannerImage.Replace("wwwroot", "")}/{BannerImageNumber}.jpg";
    }
}
