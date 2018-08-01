﻿using System;
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
    public class Client
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ProfilePhotoUrl { get; set; }
        public string Residence { get; set; }
        public string PhoneNumber { get; set; }
    }
}