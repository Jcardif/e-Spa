using Microsoft.WindowsAzure.MobileServices;

namespace e_SpaMobileApp.ServiceModels
{
    public class Salon
    {
        public string Id { get; set; }
        public string Deescription { get; set; }
        public string Locality { get; set; }
        public string ImageUrl { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public int Rating { get; set; }
        public string TimeIn { get; set; }
        public string TimeOut { get; set; }
        public string SalonManager_Id { get; set; }

        [Version] public string AzureVersion { get; set; }

        [CreatedAt] public string AzureCreated { get; set; }

        [UpdatedAt] public string AzureUpdated { get; set; }

        [Deleted] public string AzureDeleted { get; set; }
    }
}