using System;

namespace BooksWorld.Entity
{
    public class Offer
    {
        public int Id { get; set; }
        public double Percentage { get; set; }
        public bool Status { get; set; }
        public double Amount { get; set; }
        public int SearchId { get; set; }
        public OfferCategory OfferCategory { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
