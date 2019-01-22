
namespace BooksWorld.Entity
{
    public class BooksWorldSystemSetting
    {
        public int Id { get; set; }
        public string Logo { get; set; }
        public string AboutUs { get; set; }
        public string HomeHeader { get; set; }
        public double ServiceRating { get; set; }
        public int RentPercentage { get; set; }
        public double ShipmentCharge { get; set; }
        public User user { get; set; }
    }
}
