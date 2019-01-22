using BooksWorld.Data.Interface;
using BooksWorld.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace BooksWorld.Data.Repository
{
     public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public bool Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public Author GetById(int id)
        {
            return context.Authors.FirstOrDefault(s => s.Id == id);
        }

        public IEnumerable<Book> GetAllBooksById(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Author> GetAllBookByName(string name)
        {
            return context.Authors.Include(s => s.Books).Where(s => s.Name.Trim().ToLower().Equals(name.ToLower()));
        }

        public Author GetByEmail(string email)
        {
            return base.context.Authors.Where(s => s.Email.Equals(email)).SingleOrDefault();
        }

        public IEnumerable<Author> GetByName(string name)
        {
            return base.context.Authors.Where(s => s.Name.Equals(name)).ToList();
        }
        
    }
}
