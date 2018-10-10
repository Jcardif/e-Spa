using System.ComponentModel.DataAnnotations;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using Syncfusion.Android.DataForm;

namespace e_SpaMobileApp.ServiceModels
{
    public class Client
    {
        [Display(AutoGenerateField = false)]
        public string Id { get; set; }
        [Display(AutoGenerateField = false)]
        public string FirstName { get; set; }
        [Display(AutoGenerateField = false)]
        public string LastName { get; set; }
        [Display(Name = "Email"), Required(AllowEmptyStrings = false, ErrorMessage = "Email Cannot be Empty"), DisplayOptions(ImageSource = Resource.Drawable.ic_email_white_24dp)]
        public string Email { get; set; }
        [Display(AutoGenerateField = false)]
        public string ProfilePhotoUrl { get; set; }
        [Display(Name = "Residence"), Required(AllowEmptyStrings = false, ErrorMessage = "Residence Cannot be Empty"), DisplayOptions(ImageSource = Resource.Drawable.ic_location_city_white_24dp)]
        public string Residence { get; set; }
        [Display(Name = "Phone Number(eg 712345678)"), Required(AllowEmptyStrings = false, ErrorMessage = "Phone Number Cannot be Empty"), StringLength(10, ErrorMessage = "Enter a valid Phone Number"), DisplayOptions(ImageSource = Resource.Drawable.ic_phone_white_24dp)]
        public string PhoneNumber { get; set; }

        [Version, Display(AutoGenerateField = false)] public string AzureVersion { get; set; }

        [CreatedAt, Display(AutoGenerateField = false)] public string AzureCreated { get; set; }

        [UpdatedAt, Display(AutoGenerateField = false)] public string AzureUpdated { get; set; }

        [Deleted, Display(AutoGenerateField = false)] public string AzureDeleted { get; set; }
    }
}