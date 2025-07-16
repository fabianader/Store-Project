namespace StoreProject.Common.Services
{
    public interface IFileManager
    {
        string SaveFileAndReturnName(IFormFile file, string savePath);
        string SaveImageAndReturnImageName(IFormFile file, string savePath);
        void DeleteFile(string fileName, string path);
    }
}
