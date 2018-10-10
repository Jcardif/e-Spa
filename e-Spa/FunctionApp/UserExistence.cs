using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FunctionApp.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace FunctionApp
{
    public static class UserExistence
    {
        [FunctionName("UserExistence")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");
            var connectionString = ConfigurationManager.ConnectionStrings["DataConnectionString"].ConnectionString;
            using (var conn = new SqlConnection(connectionString))
            {
                // parse query parameter
                string phoneNo = req.GetQueryNameValuePairs()
                    .FirstOrDefault(q => string.Compare(q.Key, "phoneNo", StringComparison.OrdinalIgnoreCase) == 0)
                    .Value;

                if (phoneNo == null)
                {
                    // Get request body
                    dynamic data = await req.Content.ReadAsAsync<object>();
                    phoneNo = data?.phoneNo;
                    if (phoneNo==null)
                    {
                        log.Info("Phone Number not valid");
                        return req.CreateResponse(HttpStatusCode.BadRequest, "Pass a valid phone number");
                    }
                }
                try
                {
                    await conn.OpenAsync();
                    var client = new Client();
                    var query = $"SELECT * FROM Client WHERE PhoneNumber = {phoneNo}";
                    var cmd = new SqlCommand(query, conn);
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        client.Id = reader.GetString(0);
                        client.FirstName = reader.GetString(1);
                        client.LastName = reader.GetString(2);
                        client.Email = reader.GetString(3);
                        client.PhoneNumber = reader.GetString(6);
                        client.ProfilePhotoUrl = reader.GetString(4);
                        client.Residence = reader.GetString(5);

                        log.Info($"User found as{client.FirstName} {client.LastName}");
                        return req.CreateResponse(HttpStatusCode.OK, client);
                    }
                    else
                    {
                        log.Info("User not Found");
                        return req.CreateResponse<Client>(HttpStatusCode.NotFound, null);
                    }

                }
                catch (Exception ex)
                {
                    log.Info("$The following Exception happened: {ex.Message}");
                    return req.CreateResponse(HttpStatusCode.BadRequest,
                        $"The following Exception happened: {ex.Message}");
                }
                finally
                {
                    log.Info("Completed");
                    conn.Close();
                }
            }
           
        }
    }
}
