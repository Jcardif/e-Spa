using System;

namespace e_SpaBackend.DataObjects
{
    public class SalonServiceView
    {
        public string Id { get; set; }
        public string SalonName { get; set; }
        public Double Price { get; set; }
        public string Discount { get; set; }
        public string ServiceName { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
    }
}