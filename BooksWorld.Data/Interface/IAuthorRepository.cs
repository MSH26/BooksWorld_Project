using BooksWorld.Entity;
using System.Collections.Generic;

namespace BooksWorld.Data.Interface
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Author GetById(int id);
        bool Delete(int id);
        IEnumerable<Author> GetByName(string name);
        Author GetByEmail(string email);
        IEnumerable<Book> GetAllBooksById(int id);
    }
}
