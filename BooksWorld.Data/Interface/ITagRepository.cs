using BooksWorld.Entity;

namespace BooksWorld.Data.Interface
{
    interface ITagRepository : IRepository<Tag>
    {
        Tag GetById(int id);
        bool Delete(int id);
        Tag GetByContent(string content);
    }
}
