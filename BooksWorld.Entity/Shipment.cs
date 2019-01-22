using System;

namespace BooksWorld.Entity
{
    public class Shipment
    {
        public int Id { get; set; }
        public bool Type { get; set; }
        public bool Status { get; set; }
        public string Address { get; set; }
        public DateTime ShipmentDate { get; set; }
        public User User { get; set; }
        public double ShipmentCharge { get; set; }
    }
}
