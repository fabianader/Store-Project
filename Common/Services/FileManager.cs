namespace StoreProject.Common.Services
{
    public class FileManager : IFileManager
    {
        public string SaveFileAndReturnName(IFormFile file, string savePath)
        {
            if (file == null)
                // return null;
                throw new Exception("File Is Null");

            var fileName = $"{Guid.NewGuid()}{file.FileName}";

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), savePath.Replace("/", "\\"));
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            var fullPath = Path.Combine(folderPath, fileName);

            using var stream = new FileStream(fullPath, FileMode.Create);
            file.CopyTo(stream);
            return fileName;
        }

        public void DeleteFile(string fileName, string path)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), path, fileName);
            if (File.Exists(filePath))
                File.Delete(filePath);
        }

        public string SaveImageAndReturnImageName(IFormFile file, string savePath)
        {
            var IsImage = ImageValidation.Validate(file);
            if (!IsImage)
                throw new Exception();

            return SaveFileAndReturnName(file, savePath);
        }
    }
}
