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
    public class Trend
    {
        public string SenderName { get; set; }
        public string TrendTitle { get; set; }
        public string TrendDescripton { get; set; }
        public string SenderProfileImageUrl { get; set; }

    }
}