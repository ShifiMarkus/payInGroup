using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Options;

namespace payInGroup
{
        public class GoogleStorageManagerOptions
        {
            public string? BucketName { get; set; }
            public string? CredentialsFilePath { get; set; }
        }
        public class GoogleStorageManager
        {
            private readonly GoogleStorageManagerOptions _options;
            public GoogleStorageManager(IOptions<GoogleStorageManagerOptions> options)
            {
                _options = options.Value;
            }
            public async Task<string> UploadFileAsync(IFormFile file)
            {

                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    // Upload the file if less than 2 MB
                    if (memoryStream.Length < 2097152)
                    {
                        var credential = GoogleCredential.FromFile(_options.CredentialsFilePath);
                        var storage = StorageClient.Create(credential);
                        var fileName = Guid.NewGuid().ToString();//יצירת שם יחודי לקובץ, אחרת הוא עלול להידרס
                        var extension = Path.GetExtension(file.FileName);//שליפת סיומת הקובץ
                        var uploadedObject = storage.UploadObject(_options.BucketName, $"{fileName}{extension}", "application/octet-stream", memoryStream);
                        var link = uploadedObject.SelfLink;
                        return link;
                    }
                }
                return String.Empty;
            }
        }
}
