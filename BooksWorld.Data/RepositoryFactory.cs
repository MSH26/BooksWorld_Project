using BooksWorld.Data.Interface;
using BooksWorld.Data.Repository;
using BooksWorld.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksWorld.Data
{
    public class RepositoryFactory
    {
        private readonly IDictionary<Type, Type> repositories = new Dictionary<Type, Type>();

        public RepositoryFactory()
        {
            repositories.Add(typeof(Author), typeof(AuthorRepository));
            repositories.Add(typeof(BankAccount), typeof(BankAccountRepository));
            repositories.Add(typeof(BookCategory), typeof(BookCategoryRepository));
            repositories.Add(typeof(Book), typeof(BookRepository));
            repositories.Add(typeof(BooksWorldSystemSetting), typeof(BooksWorldSystemSettingRepository));
            repositories.Add(typeof(Cart), typeof(CartRepository));
            repositories.Add(typeof(CoverageArea), typeof(CoverageAreaRepository));
            repositories.Add(typeof(Invoice), typeof(InvoiceRepository));
            repositories.Add(typeof(Membership), typeof(MembershipRepository));
            repositories.Add(typeof(OfferCategory), typeof(OfferCategoryRepository));
            repositories.Add(typeof(Offer), typeof(OfferRepository));
            repositories.Add(typeof(Order), typeof(OrderRepository));
            repositories.Add(typeof(Payment), typeof(PaymentRepository));
            repositories.Add(typeof(Publication), typeof(PublicationRepository));
            repositories.Add(typeof(Rating), typeof(RatingRepository));
            repositories.Add(typeof(Shipment), typeof(ShipmentRepository));
            repositories.Add(typeof(TagOfBook), typeof(TagOfBookRepository));
            repositories.Add(typeof(Tag), typeof(TagRepository));
            repositories.Add(typeof(User), typeof(UserRepository));
        }

        public IRepository<TEntity> Create<TEntity>() where TEntity : class
        {
            Type type = repositories[typeof(TEntity)];
            return Activator.CreateInstance(type) as IRepository<TEntity>;
        }
    }
}
