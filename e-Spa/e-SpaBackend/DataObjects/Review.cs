using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Microsoft.Azure.Mobile.Server;

namespace e_SpaBackend.DataObjects
{
    public class Review : EntityData
    {
        [Required, Column(TypeName = "varchar")]
        public string Description { get; set; }
        [Required, Column(TypeName = "varchar")]
        public string Date { get; set; }
        [Required, ForeignKey("Client")]
        public string Client_Id { get; set; }
        [Required, ForeignKey("Salon")]
        public string Salon_Id { get; set; }

        public virtual Salon Salon { get; set; }
        public virtual Client Client { get; set; }

    }
}