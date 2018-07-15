using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Microsoft.Azure.Mobile.Server;

namespace e_SpaBackend.DataObjects
{
    public class SalonService :EntityData
    {
        [Required, Column(TypeName = "VARCHAR")]
        public string ImageUrl { get; set; }
        [Required, Column(TypeName = "VARCHAR")]
        public string Description { get; set; }
        [Required,Column(TypeName = "FLOAT")]
        public  double Price { get; set; }
        [Required, Column(TypeName = "FLOAT")]
        public double Discount { get; set; }

        [ForeignKey("Salon"),Required ]
        public string Salon_Id { get; set; }
        [ForeignKey("Service"),Required ]
        public string Service_Id { get; set; }

        public virtual Service Service { get; set; }
        public virtual Salon Salon { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}