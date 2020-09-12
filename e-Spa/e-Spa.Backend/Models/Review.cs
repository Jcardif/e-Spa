using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_Spa.Backend.Models
{
    public class Review
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, Column(TypeName = "VARCHAR")]
        public string Description { get; set; }
        [Required, Column(TypeName = "VARCHAR")]
        public string Date { get; set; }

        [ForeignKey(nameof(SalonClient))]
        public int ClientId { get; set; }
        [ForeignKey(nameof(Salon))]
        public int SalonId { get; set; }

        public virtual Salon Salon { get; set; }
        public virtual SalonClient SalonClient { get; set; }

    }
}