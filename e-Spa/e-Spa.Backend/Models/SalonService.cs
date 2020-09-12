using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_Spa.Backend.Models
{
    public class SalonService
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, Column(TypeName = "VARCHAR")]
        public string ImageUrl { get; set; }
        [Required, Column(TypeName = "VARCHAR")]
        public string Description { get; set; }
        [Required,Column(TypeName = "FLOAT")]
        public  double Price { get; set; }
        [Required, Column(TypeName = "FLOAT")]
        public double Discount { get; set; }

        [ForeignKey(nameof(Salon)) ]
        public int SalonId { get; set; }
        [ForeignKey(nameof(Service)) ]
        public int ServiceId { get; set; }

        public virtual Service Service { get; set; }
        public virtual Salon Salon { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}