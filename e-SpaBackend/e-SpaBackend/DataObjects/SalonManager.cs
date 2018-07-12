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
        [Required, Column(TypeName = "varchar")]
        public string FirstName { get; set; }
        [Required, Column(TypeName = "varchar")]
        public string LastName { get; set; }
        [Required, Column(TypeName = "varchar")]
        public string Email { get; set; }
        [Required, Column(TypeName = "varchar")]
        public string ProfilePhotoUrl { get; set; }
        [Required, Column(TypeName = "varchar")]
        public string PhoneNumber { get; set; }
        [Required,ForeignKey("Salon")]
        public string Salon_Id { get; set; }

        public virtual  Salon Salon  { get; set; }
    }
}