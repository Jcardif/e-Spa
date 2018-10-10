using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace FunctionApp
{
    public static class GenerateBlobStorageSas
    {
        [FunctionName("GenerateBlobStorageSas")]
        public static  HttpResponseMessage Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");
            if (req.GetQueryNameValuePairs().FirstOrDefault().Key != "containerName")
                return req.CreateResponse(HttpStatusCode.BadRequest, "Pass a valid request key");
            var containerName = req.GetQueryNameValuePairs().FirstOrDefault().Value;
            if (containerName == null)
                return req.CreateResponse(HttpStatusCode.BadRequest, "Pass a valid container name");

            CloudStorageAccount _storageAccount=CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["BlobConnectionSstring"].ConnectionString);
            CloudBlobClient _blobClient = _storageAccount.CreateCloudBlobClient();
            CloudBlobContainer _blobContainer = _blobClient.GetContainerReference(containerName);
            if (!_blobContainer.Exists())
                return req.CreateResponse(HttpStatusCode.NotFound, "Requested Container not found");

            SharedAccessBlobPolicy _accessBlobPolicy = new SharedAccessBlobPolicy
            {
                SharedAccessExpiryTime = DateTimeOffset.UtcNow.AddMinutes(3),
                Permissions = SharedAccessBlobPermissions.Read | SharedAccessBlobPermissions.Write
            };
            var sasToken =  _blobContainer.GetSharedAccessSignature(_accessBlobPolicy);
            return req.CreateResponse(HttpStatusCode.OK, sasToken);
        }
        
    }
}
