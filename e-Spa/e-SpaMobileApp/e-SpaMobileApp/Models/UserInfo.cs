using System;
using System.ComponentModel.DataAnnotations;
using Syncfusion.Android.DataForm;

using e_SpaMobileApp.ExtensionsAndHelpers;

namespace e_SpaMobileApp.Models
{
    public class UserInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Residence { get; set; }
        
    }

    public class PhoneInfo : NotificationObject
    {
        public string CountryCode { get; set; }
        public string PhoneNumber { get; set; }
        
    }
}