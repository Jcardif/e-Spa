﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace e_Spa.Backend.Models
{
    /// <summary>
    /// Represents the Salon table on the Database with the defined properties as Table columns
    /// </summary>
    public class SalonClient
    {
        /// <summary>
        /// Primary Key on the Database
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Location of the client in Longitude and Latitude e.g "45.9, 32.9"
        /// </summary>
        [Required, Column(TypeName = "VARCHAR")]
        public string Location { get; set; }


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
        public virtual ICollection<Appointment> Appointments { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
