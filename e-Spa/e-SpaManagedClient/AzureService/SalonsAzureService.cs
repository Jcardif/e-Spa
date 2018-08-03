using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using e_SpaManagedClient.Models;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Plugin.Connectivity;

namespace e_SpaManagedClient.AzureService
{
    public class SalonAzureService
    {
        private MobileServiceClient client;
        private IMobileServiceSyncTable<Salon> salonTable;

        public async Task Initialise()
        {
            if (client?.SyncContext?.IsInitialized ?? false)
                return;
            var appUrl = "https://e-spa.azurewebsites.net/";
            client = new MobileServiceClient(appUrl);
            var fileName = "espa.db";
            var store = new MobileServiceSQLiteStore(fileName);
            store.DefineTable<Salon>();

            await client.SyncContext.InitializeAsync(store);
            salonTable = client.GetSyncTable<Salon>();

        }

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