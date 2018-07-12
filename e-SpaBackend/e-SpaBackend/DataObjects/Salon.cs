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
        public string Id { get; set; }
        public string Deescription { get; set; }
        public string Locality{ get; set; }
        public string ImageUrl { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public int Rating { get; set; }
        public string TimeIn { get; set; }
        public string TimeOut { get; set; }
        [Required, ForeignKey("SalonManager")]
        public string SalonManager_Id { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual SalonManager SalonManager { get; set; }
        public virtual ICollection<SalonService> SalonServices { get; set; }
    }
}