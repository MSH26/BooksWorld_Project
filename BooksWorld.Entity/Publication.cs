using System;
using System.Collections.Generic;

namespace BooksWorld.Entity
{
    public class Publication
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Logo { get; set; }
        public string MobileNo { get; set; }
        public DateTime AddDate { get; set; }
        public List<Book> Books { get; set; }

        public Publication()
        {
            Books = new List<Book>();
        }
    }
}
