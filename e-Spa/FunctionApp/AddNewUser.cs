using System;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using FunctionApp.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;

namespace FunctionApp
{
    public static class AddNewUser
    {
        [FunctionName("AddNewUser")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get","post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");
            var connectionString =
                Environment.GetEnvironmentVariable("DataConnectionString", EnvironmentVariableTarget.Process);
            var client = await req.Content.ReadAsAsync<Client>();
            //var client = JsonConvert.DeserializeObject<Client>(body as string);
            using (var conn=new SqlConnection(connectionString))
            {
                conn.Open();
                var query =
                    $"INSERT INTO Client (FirstName,LastName,Email,PhoneNumber,ProfilePhotoUrl,Residence,Deleted)" +
                    $" VALUES ('{client.FirstName}','{client.LastName}','{client.Email}','{client.PhoneNumber}'" +
                    $",'{client.ProfilePhotoUrl}','{client.Residence}','false')";
                var cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                return req.CreateResponse(HttpStatusCode.OK);
            }

        }
    }
}
