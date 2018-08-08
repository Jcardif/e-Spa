using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using e_SpaMobileApp.ServiceModels;
using Microsoft.WindowsAzure.MobileServices;
using Plugin.Connectivity;

namespace e_SpaMobileApp.APIClients
{
    public class SocialPlatformIdApi
    {
        private IMobileServiceTable<SocialPlatformID> socialPlatformIDTable;
        private MobileServiceClient client; 
        public SocialPlatformIdApi()
        {
            client = new MobileServiceClient("https://e-spa.azurewebsites.net/");
            socialPlatformIDTable = client.GetTable<SocialPlatformID>();
        }

        private async Task<List<SocialPlatformID>> GetSocialPlatformIdAsync()
        {
            return await socialPlatformIDTable.OrderBy(i => i.PlatformId).ToListAsync();
        }
        public  async Task<SocialPlatformID> GetSocialPlatformIdBySocialPlatformAsync(string platformId)
        {
            var idList = await GetSocialPlatformIdAsync();
            return idList.Find(i => i.PlatformId == platformId);

        }
        public async Task<bool> CheeckIfPlatforIdExistAsync(string id, SocialPlatform sp)
        {
            var idList = await GetSocialPlatformIdAsync();
            return idList.Any(i => i.PlatformId == id && i.SocialPlatform == sp);
        }

        public async Task<SocialPlatformID> AddSocialPlatformId(SocialPlatformID socialPlatformId)
        {
            await socialPlatformIDTable.InsertAsync(socialPlatformId);
            return null;
        }

    }
}