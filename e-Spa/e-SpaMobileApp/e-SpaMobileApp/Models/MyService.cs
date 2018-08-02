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
   public class MyService
    {
        public String ServiceProfileImgUrl { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public int SalonsOffering { get; set; }
        public int ServicePopularity { get; set; }
    }
}