using Microsoft.WindowsAzure.MobileServices;

namespace e_SpaMobileApp.ServiceModels
{
    public enum SocialPlatform
    {
        facebook = 0,
        google = 1,
        mail = 2
    }
    public class SocialPlatformID
    {        
        public string Id { get; set; }
        public string PlatformId { get; set; }       

        public SocialPlatform SocialPlatform { get; set; }


        [Version] public string AzureVersion { get; set; }

        [CreatedAt] public string AzureCreated { get; set; }

        [UpdatedAt] public string AzureUpdated { get; set; }

        [Deleted] public string AzureDeleted { get; set; }
    }
}