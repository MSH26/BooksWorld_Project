using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BooksWorld.Models
{
    public class AddBookModel
    {
        public string Name { get; set; }
        public int AuthorId { get; set; }
        public int PublicationId { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
    }
}