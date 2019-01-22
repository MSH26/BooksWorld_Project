using System;
using System.Collections.Generic;

namespace BooksWorld.Entity
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Author Author { get; set; }
        public Publication Publication { get; set; }
        public double Price { get; set; }
        public int AvailableQuantity { get; set; }
        public BookCategory BookCategory { get; set; }
        public string Picture { get; set; }
        public string Description { get; set; }
        public DateTime AddDate { get; set; }
        public List<User> Users { get; set; }

        public Book() {
            Users = new List<User>();
        }
    }
}
