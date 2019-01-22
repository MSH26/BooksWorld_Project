
namespace BooksWorld.Entity
{
    public class Rating
    {
        public int Id { get; set; }
        public double BookQuality { get; set; }
        public double DeliveryQuality { get; set; }
        public User User { get; set; }
        public Invoice Invoice { get; set; }
    }
}
