using System;
using System.ComponentModel.DataAnnotations;
using Syncfusion.Android.DataForm;

using e_SpaMobileApp.ExtensionsAndHelpers;

namespace e_SpaMobileApp.Models
{
    public class UserInfo
    {
        [Display(Name ="First Name"), Required(AllowEmptyStrings =false, ErrorMessage ="First Name Cannot be empty")]
       public string FirstName { get; set; }
        [Display(Name ="Last Name"), Required(AllowEmptyStrings =false, ErrorMessage ="Last Name Cannot be empty")]
       public string LastName { get; set; }
        [Display(Name ="Email"), Required(AllowEmptyStrings =false, ErrorMessage ="Email Cannot be Empty")]
        public string Email { get; set; }
        public string Residence { get; set; }
        
    }

    public class PhoneInfo : NotificationObject
    {
        [Display(Name ="Country Code"), Required(AllowEmptyStrings =false, ErrorMessage ="Select your Country Dial Code")]
        public string CountryCode { get; set; }
        [Display(Name ="Phone Number, 712345678"), Required(AllowEmptyStrings =false, ErrorMessage ="Phone Number Cannot be Empty"), StringLength(9, ErrorMessage ="Enter a valid Phone Number")]
         public string PhoneNumber { get; set; }
        
    }
}