using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using WebStore.Interfaces.Services;

namespace WebStore.Implements.Services
{
    public class FirebaseStorageService : IFirebaseStorageService
    {
        private readonly string _bucketName = "tracking-app-b5b1f.appspot.com";
        private readonly GoogleCredential _googleCredential;
        public FirebaseStorageService()
        {
            var configFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "firebase-config.json");
            _googleCredential = GoogleCredential.FromFile(configFilePath);
        }

        public async Task<bool> UploadFileAsync(IFormFile file, string fileName)
        {
            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    using (var storageClient = StorageClient.Create(_googleCredential))
                    {
                        var uploadedFile = await storageClient.UploadObjectAsync(_bucketName, fileName, file.ContentType, memoryStream);
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        [Obsolete]
        public async Task<string> GetSignedUrlAsync(string fileName, int timeOutInMinutes = 30)
        {
            try
            {
                var sac = _googleCredential.UnderlyingCredential as ServiceAccountCredential;
                var urlSigner = UrlSigner.FromServiceAccountCredential(sac);
                var signedUrl = await urlSigner.SignAsync(_bucketName, fileName, TimeSpan.FromMinutes(timeOutInMinutes));
                return signedUrl.ToString();
            }
            catch
            {
                return "";
            }
        }

        public async Task<bool> DeleteFileAsync(string fileName)
        {
            try
            {
                using (var storageClient = StorageClient.Create(_googleCredential))
                {
                    await storageClient.DeleteObjectAsync(_bucketName, fileName);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}