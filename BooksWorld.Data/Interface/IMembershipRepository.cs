using BooksWorld.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BooksWorld.Data.Interface
{
    public interface IMembershipRepository: IRepository<Membership>
    {
        
        Membership GetById(int id);
        bool Delete(int id);
        Membership GetByName(string name);

    }
}
