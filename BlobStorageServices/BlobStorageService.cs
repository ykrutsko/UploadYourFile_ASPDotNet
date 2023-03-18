using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using UploadYourFile.Services;


namespace UploadYourFile.BlobStorageServices
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly string _storageConnectionString;
        private readonly string _storageContainerName;

        public BlobStorageService(IConfiguration configuration)
        {
            _storageConnectionString = configuration.GetValue<string>("BlobConnectionString");
            _storageContainerName = configuration.GetValue<string>("BlobContainerName");
        }

        public async Task UploadBlobFileAsync(IFormFile files)
        {
            try
            {
                byte[] dataFiles;
                // Retrieve storage account from connection string.
                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(_storageConnectionString);

                // Create the blob client.
                CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

                // Retrieve a reference to a container.
                CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(_storageContainerName);

                BlobContainerPermissions permissions = new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                };
                string systemFileName = files.FileName;
                await cloudBlobContainer.SetPermissionsAsync(permissions);
                await using (var target = new MemoryStream())
                {
                    files.CopyTo(target);
                    dataFiles = target.ToArray();
                }
                CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(systemFileName);
                await cloudBlockBlob.UploadFromByteArrayAsync(dataFiles, 0, dataFiles.Length);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
