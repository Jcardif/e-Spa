using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace e_Spa.Backend.Models
{
    /// <summary>
    /// Represents the SalonService table on the Database with the defined properties as Table columns
    /// </summary>
    public class SalonService
    {
        /// <summary>
        /// Primary Key on the Database
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Image Url for the Salon Service
        /// </summary>
        [Required, Column(TypeName = "VARCHAR")]
        public string ImageUrl { get; set; }

        /// <summary>
        /// Description of the Service
        /// </summary>
        [Required, Column(TypeName = "VARCHAR")]
        public string Description { get; set; }

        /// <summary>
        /// Unit Price of the service
        /// </summary>
        [Required,Column(TypeName = "FLOAT")]
        public  double Price { get; set; }

        /// <summary>
        /// Discount of the service as a percentage 
        /// </summary>
        [Required, Column(TypeName = "FLOAT")]
        public double Discount { get; set; }

        /// <summary>
        /// Foreign Key for Table Salon
        /// </summary>
        [ForeignKey(nameof(Salon)) ]
        public int SalonId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public virtual Salon Salon { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}