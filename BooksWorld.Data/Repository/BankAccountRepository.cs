using BooksWorld.Data.Interface;
using BooksWorld.Entity;

namespace BooksWorld.Data.Repository
{
    class BankAccountRepository : Repository<BankAccount>, IBankAccountRepository
    {
        public bool Delete(int id) {
            return true;
        }
    }
}
