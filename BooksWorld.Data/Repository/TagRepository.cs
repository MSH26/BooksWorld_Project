using BooksWorld.Data.Interface;
using BooksWorld.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksWorld.Data.Repository
{
    class TagRepository : Repository<Tag>, ITagRepository
    {
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Tag GetByContent(string content)
        {
            throw new NotImplementedException();
        }

        public Tag GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
