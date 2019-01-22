using BooksWorld.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksWorld.Data.Interface
{
    interface ITagOfBookRepository : IRepository<TagOfBook>
    {
        TagOfBook GetByTagId(int id);
        TagOfBook GetByBookId(int id);
        bool DeleteByTagId(int id);
        bool DeleteByBookId(int id);
    }
}
