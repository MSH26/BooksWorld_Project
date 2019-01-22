using BooksWorld.Data.Interface;
using BooksWorld.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace BooksWorld.Data.Repository
{
    public class OfferRepository : Repository<Offer>, IOfferRepository
    {
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Offer GetByAmount(double percentage)
        {
            throw new NotImplementedException();
        }

        public Offer GetByFinishingDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        public Offer GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Offer GetByOfferCategory(int offerCategoryId)
        {
            throw new NotImplementedException();
        }

        public Offer GetByOfferingDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        public Offer GetByPercentage(double percentage)
        {
            throw new NotImplementedException();
        }

        public Offer GetByStatus(bool status)
        {
            throw new NotImplementedException();
        }
        public List<Offer> GetAllCurrentOffers()
        {
            return context.Offers.Include(s => s.OfferCategory).Where(s => s.Status == true).ToList();
        }
    }
}
