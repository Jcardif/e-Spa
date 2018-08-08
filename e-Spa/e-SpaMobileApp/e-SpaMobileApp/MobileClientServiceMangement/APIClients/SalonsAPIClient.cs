using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using e_SpaMobileApp.ServiceModels;
using Microsoft.WindowsAzure.MobileServices;
using Plugin.Connectivity;

namespace e_SpaMobileApp.APIClients
{
    public class SalonsAPIClient
    {
        private async Task SyncSalons()
        {
            await ApiClient.Initialise();
            try
            {
                if (!CrossConnectivity.Current.IsConnected)
                    return;
                await ApiClient.client.SyncContext.PushAsync();
                await ApiClient.salonTable.PullAsync("allSalons", ApiClient.salonTable.CreateQuery());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<List<Salon>> GetSalons()
        {
            await ApiClient.Initialise();
            await SyncSalons();
            var data = await ApiClient.salonTable.OrderBy(s => s.Name)
                .ToListAsync();
            return data;
        }

        public async Task<Salon> AddSalon(Salon s)
        {
            await ApiClient.Initialise();
            await ApiClient.salonTable.InsertAsync(s);
            await SyncSalons();
            return null;
        }
    }
}