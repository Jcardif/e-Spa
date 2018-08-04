using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Microsoft.Azure.Mobile.Server;

namespace e_SpaBackend.DataObjects
{
    public class Client : EntityData
    {
        [Required, Column(TypeName = "VARCHAR")]
        public string FirstName { get; set; }
        [Required, Column(TypeName = "VARCHAR")]
        public string LastName { get; set; }
        [Required, Column(TypeName = "VARCHAR")]
        public string Email { get; set; }
        [Required, Column(TypeName = "VARCHAR")]
        public string ProfilePhotoUrl { get; set; }
        [Required, Column(TypeName = "VARCHAR")]
        public string Residence { get; set; }
        [Required, Column(TypeName = "VARCHAR")]
        public string PhoneNumber { get; set; }
        [Required, ForeignKey("PlatformID")]
        public string PlatformID_Id { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual PlatformID PlatformID { get; set; }
    }
}