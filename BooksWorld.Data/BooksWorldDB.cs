namespace BooksWorld.Data
{
    using Entity;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class BooksWorldDB : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }

        public BooksWorldDB() : base("name=BooksWorldDB")
        {
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
    }
}