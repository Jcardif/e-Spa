using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Azure.Mobile.Server;

namespace e_SpaBackend.DataObjects
{
    public class SalonManager : EntityData
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ProfilePhotoUrl { get; set; }
        public string PhoneNumber { get; set; }
        public string SalonId { get; set; }
    }
}