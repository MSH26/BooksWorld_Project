using BooksWorld.Data.Interface;
using BooksWorld.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksWorld.Data.Repository
{
    class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Payment GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Payment GetByPaymentDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        public Payment GetBySource(string source)
        {
            throw new NotImplementedException();
        }

        public Payment GetByStatus(bool status)
        {
            throw new NotImplementedException();
        }

        public Payment GetByType(string type)
        {
            throw new NotImplementedException();
        }
    }
}
