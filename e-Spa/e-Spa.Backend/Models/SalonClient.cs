using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_Spa.Backend.Models
{
    public class SalonClient
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, Column(TypeName = "VARCHAR")]
        public string Residence { get; set; }

        [ForeignKey(nameof(AppUser))] 
        public int AppUserId { get; set; }

        public virtual AppUser AppUser { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}