using System;
using System.Configuration;
using System.Threading.Tasks;
using Android.Graphics;
using Android.Provider;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Path = System.IO.Path;

namespace e_SpaMobileApp.BlobStorageService
{
    public class BlobStorageService
    {
        private  CloudStorageAccount _cloudStorageAccount;
        private  CloudBlobClient _blobClient;
        private  CloudBlobContainer _blobContainer;

        public void Init(string containerName)
        {
            _cloudStorageAccount = CloudStorageAccount.Parse(
                "DefaultEndpointsProtocol=https;AccountName=espa18storage;AccountKey=ig9TWpb2nIM6YExoHwxXD0qHmbFSRPETxsdE6uJEdm8pYMWLmFQvjbFh3HNdex5BppPGNsixe+oIaBfqUS2XBg==;EndpointSuffix=core.windows.net");
            _blobClient = _cloudStorageAccount.CreateCloudBlobClient();
            _blobContainer = _blobClient.GetContainerReference(containerName);
        }

        public async Task<string> UploadToBlobStorage(string filePath, string uid)
        {
            if (uid == null) uid = Guid.NewGuid().ToString();
            var blobName = uid + Path.GetExtension(filePath);
            var blockBlob = _blobContainer.GetBlockBlobReference(blobName);
            await blockBlob.UploadFromFileAsync(filePath);
            return _blobContainer.GetBlobReference(blobName).Uri.ToString();
        }
    }
}