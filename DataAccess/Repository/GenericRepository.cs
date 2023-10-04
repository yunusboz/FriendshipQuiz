using DataAccess.Contexts;
using DataAccess.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly BaseDbContext _dbContext;

        public GenericRepository(BaseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }

        public T Get(Expression<Func<T, bool>> predicate,bool trackChanges ,string includeProperties = "")
        {
            IQueryable<T> entity = _dbContext.Set<T>();
            foreach(var item in includeProperties.Split(new char[] {',' }, StringSplitOptions.RemoveEmptyEntries)) 
            {
                entity = entity.Include(item);
            }
            return trackChanges
            ? entity.Where(predicate).SingleOrDefault()
            : entity.Where(predicate).AsNoTracking().SingleOrDefault();
        }

        public IQueryable<T> GetAll(bool trackChanges, string includeProperties = "")
        {
            IQueryable<T> entities = _dbContext.Set<T>();
            foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                entities = entities.Include(item);
            }
            return trackChanges
            ? entities
            : entities.AsNoTracking();
        }

        public void Remove(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities); 
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }
    }
}
