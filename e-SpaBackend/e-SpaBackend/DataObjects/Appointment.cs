using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Microsoft.Azure.Mobile.Server;

namespace e_SpaBackend.DataObjects
{
    public class Appointment : EntityData
    {
        [Required, Column(TypeName = "varchar")]
        public string venue { get; set; }
        [Required, Column(TypeName = "varchar")]
        public string Date { get; set; }
        [Required, ForeignKey("SalonService")]
        public string SalonService_Id { get; set; }
        [Required, ForeignKey("Client")]
        public string Client_Id { get; set; }
        [Required, ForeignKey("Salon")]
        public string Salon_Id { get; set; }

        public virtual SalonService SalonService { get; set; }
        public virtual Salon Salon { get; set; }
        public virtual Client Client { get; set; }
    }
}