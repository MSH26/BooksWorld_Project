using BooksWorld.Data.Interface;
using BooksWorld.Entity;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;

namespace BooksWorld.Data.Repository
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Cart> GetAllByUserId(int userId)
        {
            return context.Carts.Include(s => s.Book).Include(s => s.User).Include(s => s.Book.BookCategory).Where(s => s.User.Id == userId).ToList();
        }
    }
}