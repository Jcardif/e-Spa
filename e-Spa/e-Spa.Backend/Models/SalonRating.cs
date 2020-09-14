using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace e_Spa.Backend.Models
{

    /// <summary>
    /// Represents the SalonRating table on the Database with the defined properties as Table columns
    /// </summary>
    public class SalonRating
    {
        /// <summary>
        /// Primary Key on the Database
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Foreign Key for Table SalonClient
        /// </summary>
        [ForeignKey(nameof(SalonClient))]
        public int? SalonClientId { get; set; }

        /// <summary>
        /// Foreign Key for Table Salon
        /// </summary>
        [ForeignKey(nameof(Salon))]
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
        public virtual SalonClient SalonClient { get; set; }

    }
}
