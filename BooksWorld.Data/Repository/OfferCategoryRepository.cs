﻿using BooksWorld.Data.Interface;
using BooksWorld.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksWorld.Data.Repository
{
    class OfferCategoryRepository : Repository<OfferCategory>, IOfferCategoryrepository
    {
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public OfferCategory GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
