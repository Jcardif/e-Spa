using System.Linq;
using System.Threading.Tasks;
using e_SpaMobileApp.ServiceModels;
using Microsoft.WindowsAzure.MobileServices;
using Plugin.Connectivity;

namespace e_SpaMobileApp.APIClients
{
    class SocialPlatformIdAPI
    {
        private IMobileServiceTable<SocialPlatformID> socialPlatformIDTable;
        public SocialPlatformIdAPI()
        {
            GetSocialPlatformIdTable();
        }

        private async void GetSocialPlatformIdTable()
        {
            if (!CrossConnectivity.Current.IsConnected)
                return;
            await ApiClient.Initialise();
            socialPlatformIDTable = ApiClient.client.GetTable<SocialPlatformID>();
        }
        public async Task<bool> CheeckIfPlatforIdExistAsync(string id, SocialPlatform sp)
        {
            var idList = await socialPlatformIDTable.OrderBy(i => i.PlatformId).ToListAsync();
            return idList.Any(i => i.PlatformId == id && i.SocialPlatform == sp);
        }

        public async Task<SocialPlatformID> AddSocialPlatformId(SocialPlatformID socialPlatformId)
        {
            await socialPlatformIDTable.InsertAsync(socialPlatformId);
            return null;
        }

    }
}