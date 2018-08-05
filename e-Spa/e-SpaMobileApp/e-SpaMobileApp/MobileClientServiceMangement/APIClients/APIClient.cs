using System.Threading.Tasks;
using e_SpaMobileApp.ServiceModels;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;

namespace e_SpaMobileApp.APIClients
{
    public static class ApiClient
    {
        public static MobileServiceClient client;
        public static IMobileServiceSyncTable<Salon> salonTable;

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