// This is the default URL for triggering event grid function in the local environment.
// http://localhost:7071/admin/extensions/EventGridExtensionConfig?functionName={functionname} 

using System;
using System.Configuration;
using FunctionApp.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FunctionApp
{
    public static class HandleBlobDeletion
    {
        [FunctionName("HandleBlobDeletion")]
        public static void Run([EventGridTrigger]JObject eventGridEvent, ILogger log)
        {
            //log.LogInformation(eventGridEvent.ToString(Formatting.Indented));
            var eventGridModel = JsonConvert.DeserializeObject<EventGridModel>(eventGridEvent.ToString());

            log.LogInformation($"HandleBlobDeletion Function processed  {eventGridModel.Data.Url} has been deleted");

            var mediumBlobUri =
                eventGridModel.Data.Url.Replace("espa-clients-profle-images", "espa-clients-profle-images-md");
            var smallBlobUri =
                eventGridModel.Data.Url.Replace("espa-clients-profle-images", "espa-clients-profle-images-sm");

            var storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["BlobConnectionSstring"].ConnectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();
            var blobMedium = blobClient.GetBlobReferenceFromServer(new Uri(mediumBlobUri));
            blobMedium.Delete();
            log.LogInformation("Deleted in medium resoluion container");
            var blobSmall = blobClient.GetBlobReferenceFromServer(new Uri(smallBlobUri));
            blobSmall.Delete();
            log.LogInformation("Deleted in Small reslution Container");
        }
    }
}
