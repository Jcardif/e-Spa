using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Microsoft.Azure.Mobile.Server;

namespace e_SpaBackend.DataObjects
{
    public class SalonManager : EntityData
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
        public string PhoneNumber { get; set; }

        public virtual  ICollection<Salon> Salons  { get; set; }
    }
}