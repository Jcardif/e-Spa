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

namespace e_SpaMobileApp.Models
{
    public class MySalon
    {
        public string SalonProfilePicUrl { get; set; }
        public string SalonName { get; set; }
        public string Location { get; set; }
        public string PhoneNumber { get; set; }
        public string SalonManager { get; set; }
    }
}