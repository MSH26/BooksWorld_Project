using System;
using System.Collections.Generic;

namespace BooksWorld.Entity
{
    public class BookCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime AddDate { get; set; }
        public List<Book> Books { get; set; }

        public BookCategory()
        {
            Books = new List<Book>();
        }
    }
}
