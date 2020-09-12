using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_Spa.Backend.Models
{
    public class SalonManager
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey(nameof(AppUser))]
        public int AppUserId { get; set; }

        public virtual AppUser AppUser { get; set; }
        public virtual  ICollection<Salon> Salons  { get; set; }
    }
}