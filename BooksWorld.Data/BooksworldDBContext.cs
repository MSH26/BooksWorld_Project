namespace BooksWorld.Data
{
    using Entity;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class BooksworldDBContext : DbContext
    {
        // Your context has been configured to use a 'BooksworldDBContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'BooksWorld.Data.BooksworldDBContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'BooksworldDBContext' 
        // connection string in the application configuration file.


        public DbSet<User> Users { get; set; }
        //public DbSet<UserDesignation> UserDesignations { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publication> Publications { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<OfferCategory> OfferCategories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<CoverageArea> CoverageAreas { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<TagOfBook> TagOfBooks { get; set; }
        public DbSet<BooksWorldSystemSetting> BooksWorldSystemSettings { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }


        public BooksworldDBContext()
            : base("name=BooksworldDBContext")
        {
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}