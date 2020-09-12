using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace e_Spa.Backend.Models
{
    /// <summary>
    /// Represents the Salon table on the Database with the defined properties as Table columns
    /// </summary>
    public class Salon
    {
        /// <summary>
        /// Primary Key on the Database
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Info about the salon
        /// </summary>
        [Required, Column(TypeName = "VARCHAR")]
        public string Info { get; set; }

        /// <summary>
        /// Location of the salon in Longitude and Latitude e.g "45.9, 32.9"
        /// </summary>
        [Required, Column(TypeName = "VARCHAR")]
        public string Location{ get; set; }

        /// <summary>
        /// Url that point to an image of the salon
        /// </summary>
        [Required, Column(TypeName = "VARCHAR")]
        public string ImageUrl { get; set; }

        /// <summary>
        /// Name of the Salon
        /// </summary>
        [Required, Column(TypeName = "VARCHAR")]
        public string Name { get; set; }

        /// <summary>
        /// Rating of the Salon
        /// </summary>
        [Required, Column(TypeName = "INT")]
        public int Rating { get; set; }

        /// <summary>
        /// Time the salon opens
        /// </summary>
        [Required, Column(TypeName = "VARCHAR")]
        public string OpenTime { get; set; }

        /// <summary>
        /// Time the salon opens
        /// </summary>
        [Required, Column(TypeName = "VARCHAR")]
        public string CloseTime { get; set; }

        /// <summary>
        /// Foreign Key for Table SalonManager
        /// </summary>
        [ForeignKey(nameof(SalonManager))]
        public int SalonManagerId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public virtual ICollection<Appointment> Appointments { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public virtual ICollection<Review> Reviews { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public virtual SalonManager SalonManager { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public virtual ICollection<SalonService> SalonServices { get; set; }
    }
}