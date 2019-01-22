using BooksWorld.Entity;


namespace BooksWorld.Data.Interface
{
    interface IBankAccountRepository: IRepository<BankAccount>
    {
        bool Delete(int id);
    }
}
