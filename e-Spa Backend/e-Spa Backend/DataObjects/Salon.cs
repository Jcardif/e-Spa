using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Microsoft.Azure.Mobile.Server;

namespace e_Spa_Backend.DataObjects
{
    public class Salon : EntityData
    {
        [Column(TypeName = "varchar"), Required]
        public string Name { get; set; }
        [Column(TypeName = "text"), Required]
        public string Description { get; set; }
        [Column(TypeName = "varchar"), Required]
        public string Locality { get; set; }
        [Column(TypeName = "varchar"), Required]
        public string Image { get; set; }
        [Column(TypeName = "int"), Required]
        public int Rating { get; set; }
        [Column(TypeName = "varchar"), Required]
        public string TimeIn { get; set; }
        [Column(TypeName = "varchar"), Required]
        public string TimeOut { get; set; }
        [Required, ForeignKey("SalonManager")]
        public string SalonManager_Id { get; set; }

        public virtual SalonManager SalonManager { get; set; }

    }
}