using System;
using System.Collections.Generic;

namespace BooksWorld.Entity
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Picture { get; set; }
        public string MobileNo { get; set; }
        public DateTime AddDate { get; set; }

        public List<Book> Books { get; set; }

        public Author()
        {
            Books = new List<Book>();
        }
    }
}
