using BooksWorld.Data.Interface;
using BooksWorld.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace BooksWorld.Data.Repository
{
    public class BookCategoryRepository : Repository<BookCategory>, IBookCategoryRepository
    {
        public BookCategory GetById(int id)
        {
            return context.BookCategories.FirstOrDefault(s => s.Id == id);
        }

        public IEnumerable<BookCategory> GetAllWithBooks()
        {
            return context.BookCategories.Include(s => s.Books).ToList();
                          
        }

        public BookCategory GetAllBooksWithId(int id)
        {
            return context.BookCategories.Include(s => s.Books).Where(s => s.Id == id).SingleOrDefault();
        }

        public bool Delete(int id)
        {
            return true;
        }
    }
}
