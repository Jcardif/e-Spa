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
    public class Review
    {
        public string Description { get; set; }
        public string Date { get; set; }
        public string Client_Id { get; set; }
        public string Salon_Id { get; set; }
    }
}