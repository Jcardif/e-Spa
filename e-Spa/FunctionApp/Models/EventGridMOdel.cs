using System;
using Newtonsoft.Json;

namespace FunctionApp.Models
{
    public class EventGridModel
    {
            [JsonProperty(PropertyName= "topic")]
            public string Topic { get; set; }
            [JsonProperty(PropertyName= "subject")]
            public string Subject { get; set; }
            [JsonProperty(PropertyName= "eventType")]
            public string EventType { get; set; }
            [JsonProperty(PropertyName= "eventTime")]
            public DateTime EventTime { get; set; }
            [JsonProperty(PropertyName= "id")]
            public string Id { get; set; }
            [JsonProperty(PropertyName= "data")]
            public Data Data { get; set; }
            [JsonProperty(PropertyName= "dataVersion")]
            public string DataVersion { get; set; }
            [JsonProperty(PropertyName= "metadataVersion")]
            public string MetaDataVersion { get; set; }
    }

        public class Data
        {
            [JsonProperty(PropertyName= "api")]
            public string Api { get; set; }
            [JsonProperty(PropertyName= "clientRequestId")]
            public string ClientRequestId { get; set; }
            [JsonProperty(PropertyName= "requestId")]
            public string RequestId { get; set; }
            [JsonProperty(PropertyName= "eTag")]
            public string ETag { get; set; }
            [JsonProperty(PropertyName= "contentType")]
            public string ContentType { get; set; }
            [JsonProperty(PropertyName= "contentLength")]
            public int ContentLength { get; set; }
            [JsonProperty(PropertyName= "blobType")]
            public string BlobType { get; set; }
            [JsonProperty(PropertyName= "url")]
            public string Url { get; set; }
            [JsonProperty(PropertyName= "sequencer")]
            public string Sequencer { get; set; }
            [JsonProperty(PropertyName= "storageDiagnostics")]
            public Storagediagnostics StorageDiagnostics { get; set; }
    }

        public class Storagediagnostics
        {
            [JsonProperty(PropertyName= "batchId")]
            public string BatchId { get; set; }
    }

}
