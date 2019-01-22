
namespace BooksWorld.Entity
{
    public class TagOfBook
    {
        public int Id { get; set; }
        public Tag Tag { get; set; }
        public Book Book { get; set; }
    }
}
