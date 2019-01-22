using System;
using System.Collections.Generic;

namespace BooksWorld.Entity
{
    public class Invoice
    {
        public int Id { get; set; }
        public double GrandPrice { get; set; }
        public bool Status { get; set; }
        public string PinNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public User User { get; set; }
        public Payment Payment { get; set; }
        public Shipment Shipment { get; set; }
        public List<Order> Orders { get; set; }

        public Invoice()
        {
            Orders = new List<Order>();
        }
    }
}
