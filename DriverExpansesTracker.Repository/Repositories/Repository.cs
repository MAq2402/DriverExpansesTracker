using DriverExpansesTracker.Repository.Entities.Base;
using DriveTracker.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DriverExpansesTracker.Repository.Repositories
{
    public class Repository<T> :IRepository<T> where T : class
    {
        private AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }
        public virtual void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public virtual void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public T FindSingleBy(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }

        public virtual IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }


        public bool Save()
        {
            if(_context.SaveChanges()>0)
            {
                return true;
            }
            return false;
        }

        public virtual void Update(T entity)
        {
           
        }
    }
}
