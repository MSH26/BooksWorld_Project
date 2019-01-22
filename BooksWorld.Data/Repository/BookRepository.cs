using BooksWorld.Data.Interface;
using BooksWorld.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace BooksWorld.Data.Repository
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public bool InsertWithAuthorPublicationCategory(Book newBook, int authorId, int publicationId, int bookCategoryId)
        {
            Author authorToAdd = context.Authors.FirstOrDefault(s => s.Id == authorId);
            Publication publicationToAdd = context.Publications.FirstOrDefault(s => s.Id == publicationId);
            BookCategory bookCategoryToAdd = context.BookCategories.FirstOrDefault(s => s.Id == bookCategoryId);
            newBook.Author = authorToAdd;
            newBook.Publication = publicationToAdd;
            newBook.BookCategory = bookCategoryToAdd;
            context.Books.Add(newBook);
            return context.SaveChanges() > 0;
        }
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Book GetById(int id) 
        {
            return context.Books.Include(s => s.Author).Include(s=> s.Publication).Include(s=> s.BookCategory).FirstOrDefault(s => s.Id == id);
        }
        
        public IEnumerable<Book> GetAllByCategoryId(int id)
        {
            return context.Books.Include(s => s.Author).Include(s => s.Publication).Where(s => s.BookCategory.Id == id).ToList(); 
        }

        public IEnumerable<Book> GetAllByCategory()
        {
            return context.Books.Include(s => s.Author).Include(s => s.Publication).Include(s => s.BookCategory).ToList();
        }


        public Book GetByAuthor(string author)
        {
            throw new NotImplementedException();
        }

        public Book GetByCategory(string category)
        {
            throw new NotImplementedException();
        }

        public Book GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> GetAllByName(string name)
        {
            return context.Books.Where(s => s.Name.ToLower().Equals(name.ToLower())).ToList();
        }

        public Book GetByPublisher(string publisher)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> GetAllBookByAuthorName(string name)
        {
            return context.Books.Include(s => s.Author).Include(s => s.Publication).Include(s => s.BookCategory).Where(s => s.Author.Name.ToLower().Equals(name.ToLower())).ToList();
        }

        public IEnumerable<Book> GetAllBookByPublicationName(string name)
        {
            return context.Books.Include(s => s.Author).Include(s => s.Publication).Include(s => s.BookCategory).Where(s => s.Publication.Name.ToLower().Equals(name.ToLower())).ToList();
        }

        public IEnumerable<Book> GetAllBookByCategoryName(string name)
        {
            return context.Books.Include(s => s.Author).Include(s => s.Publication).Include(s => s.BookCategory).Where(s => s.BookCategory.Name.ToLower().Equals(name.ToLower())).ToList();
        }
    }
}
