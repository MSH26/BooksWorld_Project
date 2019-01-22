using BooksWorld.Entity;

namespace BooksWorld.Data.Interface
{
    interface IRatingRepository : IRepository<Rating>
    {
        bool Get(int id);
        bool Delete(int id);
        Rating GetByUser(int id);
        Rating GetByInvoice(int id);
    }
}
