using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_Spa.Backend.Models
{
    public class Service
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, Column(TypeName = "VARCHAR")]
        public string Name { get; set; }
        [Required, Column(TypeName = "VARCHAR")]
        public string Description { get; set; }
        [Required, Column(TypeName = "VARCHAR")]
        public string ImageUrl { get; set; }

        public virtual ICollection<SalonService> SalonServices { get; set; }
    }
}