﻿using Microsoft.WindowsAzure.MobileServices;

namespace e_SpaMobileApp.ServiceModels
{
    public class Review
    {
        public string Description { get; set; }
        public string Date { get; set; }
        public string Client_Id { get; set; }
        public string Salon_Id { get; set; }

        [Version] public string AzureVersion { get; set; }

        [CreatedAt] public string AzureCreated { get; set; }

        [UpdatedAt] public string AzureUpdated { get; set; }

        [Deleted] public string AzureDeleted { get; set; }
    }
}