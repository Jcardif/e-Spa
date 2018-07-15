using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace e_SpaBackend.DataObjects
{
    public class ClientAppointments
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Date { get; set; }
        public string Venue { get; set; }
        public Double Price { get; set; }
        public Double Discount { get; set; }
        public string SalonName { get; set; }
        public string ServiceName { get; set; }
        public string Image { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string SMFirstName { get; set; }
        public string SMLastName { get; set; }
    }
}