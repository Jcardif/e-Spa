using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace e_Spa.Backend.Models
{
    public class AppUser : IdentityUser<int>
    {
        [Required, Column(TypeName = "VARCHAR")]
        public string FirstName { get; set; }
        [Required, Column(TypeName = "VARCHAR")]
        public string LastName { get; set; }
        [Required, Column(TypeName = "VARCHAR")]
        public string ProfilePhotoUrl { get; set; }

        public virtual ICollection<SalonClient> SalonClients { get; set; }
        public virtual ICollection<SalonManager> SalonManagers { get; set; }
    }
}
