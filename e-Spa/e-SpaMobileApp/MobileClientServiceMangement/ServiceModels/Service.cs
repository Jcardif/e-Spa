using Microsoft.WindowsAzure.MobileServices;

namespace e_SpaMobileApp.ServiceModels
{
    public class Service
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        [Version] public string AzureVersion { get; set; }

        [CreatedAt] public string AzureCreated { get; set; }

        [UpdatedAt] public string AzureUpdated { get; set; }

        [Deleted] public string AzureDeleted { get; set; }
    }
}