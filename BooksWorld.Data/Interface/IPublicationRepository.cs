using BooksWorld.Entity;

namespace BooksWorld.Data.Interface
{
    public interface IPublicationRepository : IRepository<Publication>
    {
        Publication GetById(int publicationId);
        bool Delete(int id);
        Publication GetByAddress(string address);
        Publication GetByEmail(string email);
        Book GetAllBooks(int id);
    }
}
