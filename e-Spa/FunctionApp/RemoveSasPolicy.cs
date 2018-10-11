using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Timer = System.Threading.Timer;

namespace FunctionApp
{
    public static class RemoveSasPolicy
    {
        [FunctionName("RemoveSasPolicy")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");
           
            var policyKey = req.GetQueryNameValuePairs()
                .FirstOrDefault(q => string.Compare(q.Key, "policyKey", StringComparison.OrdinalIgnoreCase) == 0)
                .Value;

            if (policyKey == null)
            {
                // Get request body
                dynamic data = await req.Content.ReadAsAsync<object>();
                policyKey = data?.policyKey;
                if (policyKey == null)
                {
                    log.Info("policy Key not valid");
                    return req.CreateResponse(HttpStatusCode.BadRequest, "Pass a valid policy Key");
                }
            }

            log.Info("Waiting to remove");
            Thread.Sleep(105000);
            log.Info("Time to remove");
            var storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["BlobConnectionSstring"].ConnectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();
            var blobContainer = blobClient.GetContainerReference("espa-clients-profle-images");
            if (!blobContainer.Exists())
                return req.CreateResponse(HttpStatusCode.NotFound, "Requested Container not found");
            var permissions = blobContainer.GetPermissions();
            if (permissions.SharedAccessPolicies.ContainsKey(policyKey))
            {
                permissions.SharedAccessPolicies.Remove(policyKey);
                blobContainer.SetPermissions(permissions);
                return req.CreateResponse(HttpStatusCode.OK, "Policy Key Removed");
            }
            log.Info("policy Key  not found");
            return req.CreateResponse(HttpStatusCode.NotFound, "Policy Key Not Found");
        }
        
    }
}
