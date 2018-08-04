using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using e_SpaMobileApp.ServiceModels;
using Microsoft.WindowsAzure.MobileServices;
using Plugin.Connectivity;
using static e_SpaMobileApp.AzureClientServices.ApiClient;

namespace e_SpaMobileApp.AzureClientServices
{
    public class SalonsAPIClient
    {        
        public async Task SyncSalons()
        {
            await Initialise();
            try
            {
                if (!CrossConnectivity.Current.IsConnected)
                    return;
                await client.SyncContext.PushAsync();
                await salonTable.PullAsync("allSalons", salonTable.CreateQuery());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<List<Salon>> GetSalons()
        {
            await Initialise();
            await SyncSalons();
            var data = await salonTable.OrderBy(s => s.Name)
                .ToListAsync();
            return data;
        }

        public async Task<Salon> AddSalon(Salon s)
        {
            await Initialise();
            await salonTable.InsertAsync(s);
            await SyncSalons();
            return null;
        }
    }
}