using System.Configuration;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace e_SpaMobileApp.BlobStorageService
{
    public class BlobStorageService
    {
        private static CloudStorageAccount _cloudStorageAccount;
        private static CloudBlobClient _blobClient;
        private static CloudBlobContainer _blobContainer;
        private static CloudBlockBlob _blockBlob;
        public static async void InitBlobStorageService()
        {
            _cloudStorageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["BlobConnectionSstring"].ConnectionString);
            _blobClient = _cloudStorageAccount.CreateCloudBlobClient();
            await InitStorageAccount();
        }

        private static async Task InitStorageAccount()
        {
            _blobContainer = _blobClient.GetContainerReference("profile-photos");
            await _blobContainer.CreateIfNotExistsAsync();
            _blockBlob = _blobContainer.GetBlockBlobReference("espa18storage");
        }

        public static async Task PostToBlob()
        {

        }
    }
}