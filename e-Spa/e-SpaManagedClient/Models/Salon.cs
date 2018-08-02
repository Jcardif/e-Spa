using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace e_SpaManagedClient.Models
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
        
        [Microsoft.WindowsAzure.MobileServices.Version]
        public string AzureVersion { get; set; }
        [Microsoft.WindowsAzure.MobileServices.CreatedAt]
        public string AzureCreated { get; set; }
        [Microsoft.WindowsAzure.MobileServices.UpdatedAt]
        public string AzureUpdated { get; set; }
        [Microsoft.WindowsAzure.MobileServices.Deleted]
        public string AzureDeleted { get; set; }
    }
}