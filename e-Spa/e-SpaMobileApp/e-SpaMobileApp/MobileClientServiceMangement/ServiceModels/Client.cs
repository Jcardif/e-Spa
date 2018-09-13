using System.ComponentModel.DataAnnotations;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace e_SpaMobileApp.ServiceModels
{
    public class Client
    {
        [Display(AutoGenerateField = false)]
        public string Id { get; set; }
        [Display(Name ="First Name"), Required(AllowEmptyStrings =false, ErrorMessage ="First Name Cannot be empty")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name"), Required(AllowEmptyStrings = false, ErrorMessage = "Last Name Cannot be empty")]
        public string LastName { get; set; }
        [Display(Name = "Email"), Required(AllowEmptyStrings = false, ErrorMessage = "Email Cannot be Empty")]
        public string Email { get; set; }
        [Display(AutoGenerateField = false)]
        public string ProfilePhotoUrl { get; set; }
        [Display(Name = "Residence"), Required(AllowEmptyStrings = false, ErrorMessage = "Residence Cannot be Empty")]
        public string Residence { get; set; }
        [Display(Name = "Phone Number(eg 712345678)"), Required(AllowEmptyStrings = false, ErrorMessage = "Phone Number Cannot be Empty"), StringLength(10, ErrorMessage = "Enter a valid Phone Number")]
        public string PhoneNumber { get; set; }

        [Version, Display(AutoGenerateField = false)] public string AzureVersion { get; set; }

        [CreatedAt, Display(AutoGenerateField = false)] public string AzureCreated { get; set; }

        [UpdatedAt, Display(AutoGenerateField = false)] public string AzureUpdated { get; set; }

        [Deleted, Display(AutoGenerateField = false)] public string AzureDeleted { get; set; }
    }
}