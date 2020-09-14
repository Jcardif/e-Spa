using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace e_Spa.Backend.Models
{
    /// <summary>
    /// Represents the Appointment table on the Database with the defined properties as Table columns
    /// </summary>
    public class Appointment
    {
        /// <summary>
        /// Primary Key on the Database
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Appointment Date
        /// </summary>
        [Required, Column(TypeName = "VARCHAR(64)")]
        public string Date { get; set; }

        /// <summary>
        /// Foreign Key for Table SalonService
        /// </summary>
        [ForeignKey(nameof(SalonService))]
        public int? SalonServiceId { get; set; }

        /// <summary>
        /// Foreign Key for Table AppUser
        /// </summary>
        [ForeignKey(nameof(SalonClient))]
        public int SalonClientId { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public virtual SalonService SalonService { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public virtual SalonClient SalonClient { get; set; }
    }
}