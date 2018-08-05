using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Microsoft.Azure.Mobile.Server;

namespace e_SpaBackend.DataObjects
{
    public class Service :EntityData
    {
        [Required, Column(TypeName = "VARCHAR")]
        public string Name { get; set; }
        [Required, Column(TypeName = "VARCHAR")]
        public string Description { get; set; }
        [Required, Column(TypeName = "VARCHAR")]
        public string ImageUrl { get; set; }

        public virtual ICollection<SalonService> SalonServices { get; set; }
    }
}