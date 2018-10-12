// This is the default URL for triggering event grid function in the local environment.
// http://localhost:7071/admin/extensions/EventGridExtensionConfig?functionName={functionname} 

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FunctionApp
{
    public static class HandleBlobDeletion
    {
        [FunctionName("HandleBlobDeletion")]
        public static void Run([EventGridTrigger]JObject eventGridEvent, ILogger log)
        {
            log.LogInformation(eventGridEvent.ToString(Formatting.Indented));
        }
    }
}
