using BooksWorld.Data.Interface;
using BooksWorld.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksWorld.Data.Repository
{
    class RatingRepository : Repository<Rating>, IRatingRepository
    {
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Get(int id)
        {
            throw new NotImplementedException();
        }

        public Rating GetByInvoice(int id)
        {
            throw new NotImplementedException();
        }

        public Rating GetByUser(int id)
        {
            throw new NotImplementedException();
        }
    }
}
