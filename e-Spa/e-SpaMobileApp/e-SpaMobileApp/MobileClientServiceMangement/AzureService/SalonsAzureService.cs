﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using e_SpaMobileApp.ServiceModels;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Plugin.Connectivity;

namespace e_SpaMobileApp.AzureService
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