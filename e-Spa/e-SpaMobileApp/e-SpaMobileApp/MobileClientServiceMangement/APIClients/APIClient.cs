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
using e_SpaMobileApp.ServiceModels;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;

namespace e_SpaMobileApp.AzureClientServices
{
    public static class ApiClient
    {
        public static MobileServiceClient client;
        public static IMobileServiceSyncTable<Salon> salonTable;
        public static IMobileServiceTable<ServiceModels.PlatformID> platformIDTable;

        public static async Task Initialise()
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
    }
}