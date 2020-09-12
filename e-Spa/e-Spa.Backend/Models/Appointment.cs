using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_Spa.Backend.Models
{
    public class Appointment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, Column(TypeName = "VARCHAR")]
        public string Venue { get; set; }
        [Required, Column(TypeName = "VARCHAR")]
        public string Date { get; set; }

        [ForeignKey(nameof(SalonService))]
        public int SalonServiceId { get; set; }
        [ForeignKey(nameof(SalonClient))]
        public int ClientId { get; set; }
        [ForeignKey(nameof(Salon))]
        public int SalonId { get; set; }

        public virtual SalonService SalonService { get; set; }
        public virtual Salon Salon { get; set; }
        public virtual SalonClient SalonClient { get; set; }
    }
}