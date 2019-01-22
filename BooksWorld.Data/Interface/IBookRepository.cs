using System;
using System.Collections.Generic;
using BooksWorld.Entity;

namespace BooksWorld.Data.Interface
{
    public interface IBookRepository: IRepository<Book>
    {
        Book GetById(int id);
        Book GetByName(string name);
        Book GetByAuthor(string author);
        Book GetByPublisher(string publisher);
        Book GetByCategory(string category);
        IEnumerable<Book> GetAllByCategoryId(int id);
        IEnumerable<Book> GetAllByCategory();
        bool Delete(int id);
        IEnumerable<Book> GetAllByName(string name);
        IEnumerable<Book> GetAllBookByCategoryName(string name);
        IEnumerable<Book> GetAllBookByPublicationName(string name);
        IEnumerable<Book> GetAllBookByAuthorName(string name);
        bool InsertWithAuthorPublicationCategory(Book newBook, int authorId, int publicationId, int bookCategoryId);
    }
}
