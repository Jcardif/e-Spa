using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Azure.Mobile.Server;

namespace e_SpaBackend.DataObjects
{
    public class Salon : EntityData
    {
        public string Id { get; set; }
        public string Deescription { get; set; }
        public string Locality{ get; set; }
        public string ImageUrl { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public int Rating { get; set; }
        public string TimeIn { get; set; }
        public string TimeOut { get; set; }
    }
}