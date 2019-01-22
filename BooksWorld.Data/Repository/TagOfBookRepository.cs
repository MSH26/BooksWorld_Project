using BooksWorld.Data.Interface;
using BooksWorld.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksWorld.Data.Repository
{
    class TagOfBookRepository : Repository<TagOfBook>, ITagOfBookRepository
    {
        public bool DeleteByBookId(int id)
        {
            throw new NotImplementedException();
        }

        public bool DeleteByTagId(int id)
        {
            throw new NotImplementedException();
        }

        public TagOfBook GetByBookId(int id)
        {
            throw new NotImplementedException();
        }

        public TagOfBook GetByTagId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
