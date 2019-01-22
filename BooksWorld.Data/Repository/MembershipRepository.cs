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
    public class MembershipRepository : Repository<Membership>, IMembershipRepository
    {
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }


        public Membership GetByName(string name)
        {
            return context.Memberships.FirstOrDefault(s=> s.Name.Equals(name));
        }
        public Membership GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
