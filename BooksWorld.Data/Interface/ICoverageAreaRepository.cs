using BooksWorld.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksWorld.Data.Interface
{
    interface ICoverageAreaRepository: IRepository<CoverageArea>
    {
        bool Delete(int id);
    }
}
