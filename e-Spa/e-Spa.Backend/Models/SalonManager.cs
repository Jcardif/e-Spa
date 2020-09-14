using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace e_Spa.Backend.Models
{
    /// <summary>
    /// Represents the SalonManager table on the Database with the defined properties as Table columns
    /// </summary>
    public class SalonManager
    {
        /// <summary>
        /// Primary Key on the Database
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Foreign Key for Table AppUser
        /// </summary>
        [ForeignKey(nameof(AppUser))]
        public int AppUserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public virtual AppUser AppUser { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public virtual ICollection<Salon> Salons { get; set; } 
    }
}
