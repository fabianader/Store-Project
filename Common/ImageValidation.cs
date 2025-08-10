namespace StoreProject.Common
{
    public class ImageValidation
    {
        public static bool Validate(string imageName)
        {
            var extension = Path.GetExtension(imageName);
            if (extension == null)
                return false;

            if (extension.ToLower() == ".jpg" || extension.ToLower() == ".png")
                return true;

            return false;
        }

        public static bool Validate(IFormFile file)
        {
            try
            {
                using var image = System.Drawing.Image.FromStream(file.OpenReadStream());
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
