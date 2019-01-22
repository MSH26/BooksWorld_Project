using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksWorld.Data.Interface
{
    public interface IRepository<TEntity> where TEntity: class
    {
        bool Insert(TEntity entity);
        bool Update(TEntity entity);
        IEnumerable<TEntity> GetAll();
       
    }
}
