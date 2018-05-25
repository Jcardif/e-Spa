using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Microsoft.Azure.Mobile.Server;

namespace e_Spa_Backend.Models
{
    public class Client : EntityData
    {
        [Required, Column(TypeName = "varchar")]
        public string Firstname { get; set; }

        [Required, Column(TypeName = "varchar")]
        public string Lastname { get; set; }
        [Column(TypeName = "varchar")]
        public string Phoneno { get; set; }
        public string PhotoUrl { get; set; }
        public string Email { get; set; }
        [ForeignKey("appoitments")]
        public int Id { get; set; }
        public ICollection<Appoitments> appoitments { get; set; }
        [NotMapped]
        public String Fullname {
            get
            {
                return Firstname + Lastname;
            }
        }

        public override string ToString()
        {
            return Fullname;
        }
    }

}