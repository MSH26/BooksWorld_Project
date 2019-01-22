using System;

namespace BooksWorld.Entity
{
    public class Order
    {
        public int Id { get; set; }
        public Book Book { get; set; }
        public bool Type { get; set; }
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public Invoice Invoice { get; set; }
        public Offer Offer { get; set; }
    }
}
