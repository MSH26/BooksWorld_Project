using BooksWorld.Data.Interface;
using BooksWorld.Entity;
using System;

namespace BooksWorld.Data.Repository
{
    class CoverageAreaRepository : Repository<CoverageArea>, ICoverageAreaRepository
    {
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
