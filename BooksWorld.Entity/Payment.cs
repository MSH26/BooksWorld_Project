using System;

namespace BooksWorld.Entity
{
    public class Payment
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Source { get; set; }
        public bool Status { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
