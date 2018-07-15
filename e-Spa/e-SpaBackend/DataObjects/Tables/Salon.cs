using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Microsoft.Azure.Mobile.Server;

namespace e_SpaBackend.DataObjects
{
    public class Salon : EntityData
    {
        [Required, Column(TypeName = "VARCHAR")]
        public string Deescription { get; set; }
        [Required, Column(TypeName = "VARCHAR")]
        public string Locality{ get; set; }
        [Required, Column(TypeName = "VARCHAR")]
        public string ImageUrl { get; set; }
        [Required, Column(TypeName = "VARCHAR")]
        public string Email { get; set; }
        [Required, Column(TypeName = "VARCHAR")]
        public string Name { get; set; }
        [Required, Column(TypeName = "INT")]
        public int Rating { get; set; }
        [Required, Column(TypeName = "VARCHAR")]
        public string TimeIn { get; set; }
        [Required, Column(TypeName = "VARCHAR")]
        public string TimeOut { get; set; }
        [Required, ForeignKey("SalonManager")]
        public string SalonManager_Id { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual SalonManager SalonManager { get; set; }
        public virtual ICollection<SalonService> SalonServices { get; set; }
    }
}