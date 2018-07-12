using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace e_SpaBackend.DataObjects
{
    public class SalonService
    {
        [Required, Column(TypeName = "varchar")]
        public string ImageUrl { get; set; }
        [Required, Column(TypeName = "varchar")]
        public string Description { get; set; }
        [Required, Column(TypeName = "double")]
        public  double Price { get; set; }
        [Required, Column(TypeName = "double")]
        public double Discount { get; set; }

        [ForeignKey("Salon"),Required ]
        public string Salon_Id { get; set; }
        [ForeignKey("Service"),Required ]
        public string Service_Id { get; set; }

        public virtual Service Service { get; set; }
        public virtual Salon Salon { get; set; }
    }
}