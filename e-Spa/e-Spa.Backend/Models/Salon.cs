using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_Spa.Backend.Models
{
    public class Salon
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, Column(TypeName = "VARCHAR")]
        public string Description { get; set; }
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
        public string OpenTime { get; set; }
        [Required, Column(TypeName = "VARCHAR")]
        public string CloseTime { get; set; }

        [ForeignKey(nameof(SalonManager))]
        public int SalonManagerId { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual SalonManager SalonManager { get; set; }
        public virtual ICollection<SalonService> SalonServices { get; set; }
    }
}