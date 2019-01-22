using System;
using System.Collections.Generic;
using BooksWorld.Entity;

namespace BooksWorld.Data.Interface
{
    public interface IOfferRepository: IRepository<Offer>
    {
       
        Offer GetById(int id);
       
        
        bool Delete(int id);
        Offer GetByPercentage(double percentage);
        Offer GetByAmount(double percentage);
        Offer GetByOfferCategory(int offerCategoryId);
        Offer GetByStatus(bool status);
        Offer GetByOfferingDate(DateTime date);
        Offer GetByFinishingDate(DateTime date);
        List<Offer> GetAllCurrentOffers();
    }
}
