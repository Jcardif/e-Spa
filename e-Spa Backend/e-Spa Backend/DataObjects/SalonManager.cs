using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Microsoft.Azure.Mobile.Server;

namespace e_Spa_Backend.DataObjects
{
    public class SalonManager : EntityData
    {
        [Required, Column(TypeName = "varchar")]
        public string FirstName { get; set; }
        [Required, Column(TypeName = "varchar")]
        public string LastName { get; set; }
        [Required, Column(TypeName = "varchar")]
        public string PhoneNo { get; set; }
        [Required, Column(TypeName = "varchar")]
        public string Email { get; set; }
        [Required, Column(TypeName = "varchar")]
        public string PhotoUrl { get; set; }

        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";

        public virtual ICollection<Salon> Salons { get; set; }
    }
}