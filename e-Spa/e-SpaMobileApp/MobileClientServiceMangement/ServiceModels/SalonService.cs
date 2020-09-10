using Microsoft.WindowsAzure.MobileServices;

namespace e_SpaMobileApp.ServiceModels
{
    public class SalonService
    {
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public string Salon_Id { get; set; }
        public string Service_Id { get; set; }

        [Version] public string AzureVersion { get; set; }

        [CreatedAt] public string AzureCreated { get; set; }

        [UpdatedAt] public string AzureUpdated { get; set; }

        [Deleted] public string AzureDeleted { get; set; }
    }
}