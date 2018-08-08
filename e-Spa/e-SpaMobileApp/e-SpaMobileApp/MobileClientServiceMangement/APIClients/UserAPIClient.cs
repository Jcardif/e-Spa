using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using e_SpaMobileApp.ServiceModels;
using Microsoft.WindowsAzure.MobileServices;
using Plugin.Connectivity;
using static e_SpaMobileApp.APIClients.ApiClient;
namespace e_SpaMobileApp.APIClients
{
    public class UserApiClient
    {
        private async Task SyncClient()
        {
            await Initialise();
            try
            {
                if(!CrossConnectivity.Current.IsConnected)
                    return;
                await client.SyncContext.PushAsync();
                await clientTable.PullAsync("allClients", clientTable.CreateQuery());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<List<Client>> GetClientsAsync()
        {
            await Initialise();
            return await clientTable.OrderBy(c => c.FirstName).ToListAsync();
        }

        public async Task<Client> AddClientAsync(Client client)
        {
            await Initialise();
            await clientTable.InsertAsync(client);
            await SyncClient();
            return null;
        }
    }
}