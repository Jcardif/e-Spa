using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Syncfusion.Android.DataForm;

namespace e_SpaMobileApp.Models
{
    public class FullName
    {
        [Display(Name = "First Name"), Required(AllowEmptyStrings = false, ErrorMessage = "First Name Cannot be empty"), DisplayOptions(ImageSource = Resource.Drawable.baseline_perm_identity_white_24dp)]
        public string FirstName { get; set; }
        [Display(Name = "Last Name"), Required(AllowEmptyStrings = false, ErrorMessage = "Last Name Cannot be empty"), DisplayOptions(ImageSource = Resource.Drawable.baseline_perm_identity_white_24dp)]
        public string LastName { get; set; }
    }
}