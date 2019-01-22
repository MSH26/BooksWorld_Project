
namespace BooksWorld.Entity
{
    public class Cart
    {
        public int Id { get; set; }
        public Book Book { get; set; }
        public User User { get; set; }
        public bool Type { get; set; }
    }
}
