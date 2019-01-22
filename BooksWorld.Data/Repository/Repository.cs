using BooksWorld.Data.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BooksWorld.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        internal BooksworldDBContext context = new BooksworldDBContext();

        IEnumerable<TEntity> IRepository<TEntity>.GetAll()
        {
            return context.Set<TEntity>().ToList();
        }

        bool IRepository<TEntity>.Insert(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
            return context.SaveChanges() > 0;
        }

        bool IRepository<TEntity>.Update(TEntity entity)
        {
            context.Entry<TEntity>(entity).State = EntityState.Modified;
            return context.SaveChanges() > 0;
        }
    }
}
