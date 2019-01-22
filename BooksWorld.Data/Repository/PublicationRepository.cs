using BooksWorld.Data.Interface;
using BooksWorld.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksWorld.Data.Repository
{
    class PublicationRepository : Repository<Publication>, IPublicationRepository
    {
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Publication GetById(int publicationId)
        {
            return context.Publications.FirstOrDefault(s => s.Id == publicationId);
        }

        public Book GetAllBooks(int id)
        {
            throw new NotImplementedException();
        }

        public Publication GetByAddress(string address)
        {
            throw new NotImplementedException();
        }

        public Publication GetByEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}
