using System;
using System.Collections.Generic;
using BooksWorld.Entity;

namespace BooksWorld.Data.Interface
{
    public interface IBookCategoryRepository: IRepository<BookCategory>
    {
        BookCategory GetById(int id);
        IEnumerable<BookCategory> GetAllWithBooks();
        BookCategory GetAllBooksWithId(int id);
        bool Delete(int id);
    }
}
