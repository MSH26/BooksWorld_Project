using System;
using System.Collections.Generic;
using BooksWorld.Entity;

namespace BooksWorld.Data.Interface
{
    public interface ICartRepository: IRepository<Cart>
    {
        
        bool Delete(int id);
        IEnumerable<Cart> GetAllByUserId(int userId);
    }
}
