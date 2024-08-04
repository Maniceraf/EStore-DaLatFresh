namespace WebStore.Interfaces.Services
{
    public interface IFirebaseStorageService
    {
        Task<bool> UploadFileAsync(IFormFile file, string fileName);
        Task<string> GetSignedUrlAsync(string fileNameToRead, int timeOutInMinutes = 30);
        Task<bool> DeleteFileAsync(string fileName);
    }
}