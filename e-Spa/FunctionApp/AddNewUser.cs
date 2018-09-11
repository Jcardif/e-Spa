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

namespace FunctionApp
{
    public static class AddNewUser
    {
        [FunctionName("AddNewUser")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]HttpRequestMessage req,[FromBody]Client client, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");
            var connectionString =
                Environment.GetEnvironmentVariable("DataConnectionString", EnvironmentVariableTarget.Process);
            using (var conn=new SqlConnection(connectionString))
            {
                conn.Open();
                var query =
                    $"INSERT INTO Client (firstName,lastName,email,phoneNumber,profilePhotoUrl,residence)" +
                    $" VALUES ({client.FirstName},{client.LastName},{client.Email},{client.PhoneNumber}" +
                    $",{client.ProfilePhotoUrl},{client.Residence}) SELECT SCOPE_IDENTITY()";
                var cmd=new SqlCommand();
                var lst = cmd.ExecuteScalar();
                return req.CreateResponse(HttpStatusCode.OK, lst);
            }

        }
    }
}
