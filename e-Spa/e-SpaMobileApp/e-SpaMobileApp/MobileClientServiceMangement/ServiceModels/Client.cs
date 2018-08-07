using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace e_SpaMobileApp.ServiceModels
{
    public class Client
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ProfilePhotoUrl { get; set; }
        public string Residence { get; set; }
        public string PhoneNumber { get; set; }
        public string SocialPlatformID_Id { get; set; }

        [JsonIgnore]
        public bool IsLoggedIn { get; set; }

        [Version] public string AzureVersion { get; set; }

        [CreatedAt] public string AzureCreated { get; set; }

        [UpdatedAt] public string AzureUpdated { get; set; }

        [Deleted] public string AzureDeleted { get; set; }
    }
}